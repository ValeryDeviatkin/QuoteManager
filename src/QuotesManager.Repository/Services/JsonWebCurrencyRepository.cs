using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QuotesManager.Interfaces;
using QuotesManager.Repository.Constants;
using QuotesManager.Repository.DataModels;
using QuotesManager.Repository.DataTransferObjects;
using QuotesManager.Repository.ExtensionMethods;
using QuotesManager.Repository.Interfaces;
using Unity;

namespace QuotesManager.Repository.Services
{
    internal class JsonWebCurrencyRepository : ICurrencyRepository
    {
        private readonly IUnityContainer _container;
        private string[] _currencyToCalculateIdList;

        public JsonWebCurrencyRepository(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        public async Task<CurrencyInfoDto> GetCurrencyAsync(string id)
        {
            var currencyMap = await LoadCurrencyListAsync();
            var currencyInfo = CreateCurrencyInfo(id, currencyMap);

            return currencyInfo;
        }

        public async Task<IEnumerable<CurrencyInfoDto>> FindCurrencyAsync(string filter)
        {
            var trimmedFilter = filter?.Trim();
            var currencyMap = await LoadCurrencyListAsync();
            var foundCurrencyIdList = new List<string>();
            var foundCurrencyList = new List<CurrencyInfoDto>();

            foreach (var currency in currencyMap.Values)
            {
                if (string.IsNullOrEmpty(trimmedFilter) ||
                    currency.CharCode.IsMatchString(trimmedFilter) ||
                    currency.NumCode.IsMatchString(trimmedFilter) ||
                    currency.Name.IsMatchString(trimmedFilter))
                {
                    foundCurrencyIdList.Add(currency.Id);
                }
            }

            foreach (var id in foundCurrencyIdList)
            {
                var currencyInfo = CreateCurrencyInfo(id, currencyMap);

                foundCurrencyList.Add(currencyInfo);
            }

            return foundCurrencyList;
        }

        public Task<decimal> ConvertCurrencyAsync(string sourceCurrencyId, decimal sourceCurrencyNominal,
                                                  string targetCurrencyId) => throw new NotImplementedException();

        public void Init(string[] currencyToCalculateIdList)
        {
            _currencyToCalculateIdList = currencyToCalculateIdList;
        }

        public async Task<IEnumerable<CurrencyPreviewDto>> GetCurrencyListAsync()
        {
            var result = new List<CurrencyPreviewDto>();
            var currencyMap = await LoadCurrencyListAsync();

            foreach (var currency in currencyMap.Values)
            {
                var currencyPreview = new CurrencyPreviewDto
                {
                    CurrencyId = currency.Id,
                    CharCode = currency.CharCode,
                    NumCode = currency.NumCode
                };

                result.Add(currencyPreview);
            }

            return result;
        }

        private CurrencyInfoDto CreateCurrencyInfo(
            string id, IReadOnlyDictionary<string, CurrencyDataModel> currencyMap)
        {
            if (!currencyMap.TryGetValue(id, out var currency))
            {
                throw new NotSupportedException();
            }

            var currencyInfo = new CurrencyInfoDto
            {
                Id = currency.Id,
                CharCode = currency.CharCode,
                NumCode = currency.NumCode,
                Name = currency.Name
            };

            var currentValue = currency.Value / currency.Nominal;
            var currencyCourseList = new List<CurrencyCourseDto>();

            foreach (var currencyToCalculateId in _currencyToCalculateIdList)
            {
                if (!currencyMap.TryGetValue(currencyToCalculateId, out var currencyToCalculate))
                {
                    throw new NotSupportedException();
                }

                var currencyToCalculateValue = currencyToCalculate.Value / currencyToCalculate.Nominal;

                var currencyCourse = new CurrencyCourseDto
                {
                    CurrencyId = currencyToCalculate.Id,
                    CharCode = currencyToCalculate.CharCode,
                    Value = currencyToCalculateValue / currentValue
                };

                currencyCourseList.Add(currencyCourse);
            }

            currencyInfo.Courses = currencyCourseList.ToArray();

            return currencyInfo;
        }

        private async Task<Dictionary<string, CurrencyDataModel>> LoadCurrencyListAsync()
        {
            using var webClient = new WebClient {Encoding = Encoding.UTF8};
            var result = new Dictionary<string, CurrencyDataModel>();
            var url = _container.Resolve<ICurrencySourceUrlProvider>().CurrencySourceUrl;
            var json = await webClient.DownloadStringTaskAsync(url);
            var jObject = JObject.Parse(json);
            var currencyListRootNode = jObject[JsonKeys.Valute];

            if (currencyListRootNode == null ||
                !currencyListRootNode.HasValues)
            {
                throw new NotSupportedException();
            }

            var currencyNodeList = currencyListRootNode.Values();

            foreach (var currencyNode in currencyNodeList)
            {
                if (!currencyNode.HasValues)
                {
                    throw new NotSupportedException();
                }

                var currency = currencyNode.ToObject<CurrencyDataModel>();

                if (currency == null)
                {
                    throw new NotSupportedException();
                }

                result.Add(currency.Id, currency);
            }

            return result;
        }
    }
}