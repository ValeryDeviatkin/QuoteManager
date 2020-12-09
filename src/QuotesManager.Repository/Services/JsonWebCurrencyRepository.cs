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
using QuotesManager.Repository.Interfaces;
using Unity;

namespace QuotesManager.Repository.Services
{
    internal class JsonWebCurrencyRepository : ICurrencyRepository
    {
        private const string ControlCurrencyId = "R01090B";
        private readonly IUnityContainer _container;
        private readonly string[] _currencyToCalculateIdList = {ControlCurrencyId, "R01235", "R01239"};

        public JsonWebCurrencyRepository(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        public async Task<CurrencyInfoDto> GetCurrencyAsync(string id)
        {
            var currencyMap = await LoadCurrencyListAsync();

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

            if (!currencyMap.TryGetValue(ControlCurrencyId, out var controlCurrency))
            {
                throw new NotSupportedException();
            }

            var controlRate = controlCurrency.Nominal / controlCurrency.Value;
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
                    Value = currentValue * currencyToCalculateValue * controlRate
                };

                currencyCourseList.Add(currencyCourse);
            }

            currencyInfo.Courses = currencyCourseList.ToArray();

            return currencyInfo;
        }

        public Task<IEnumerable<CurrencyInfoDto>> FindCurrencyAsync(string filter) =>
            throw new NotImplementedException();

        public Task<decimal> ConvertCurrencyAsync(string sourceCurrencyId, decimal sourceCurrencyNominal,
                                                  string targetCurrencyId) => throw new NotImplementedException();

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