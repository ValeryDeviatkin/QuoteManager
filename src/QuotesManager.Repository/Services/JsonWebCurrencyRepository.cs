using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly IUnityContainer _container;

        public JsonWebCurrencyRepository(IUnityContainer container)
        {
            _container = container.RegisterInstance(this);
        }

        public async Task<CurrencyDto> GetCurrencyAsync(string id)
        {
            var currencyMap = await LoadCurrencyListAsync();

            if (!currencyMap.TryGetValue(id, out var token))
            {
                throw new NotSupportedException();
            }

            var currency = new CurrencyDto();

            return currency;
        }

        public Task<IEnumerable<CurrencyDto>> FindCurrencyAsync(string filter) => throw new NotImplementedException();

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
            using var webClient = new WebClient();
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