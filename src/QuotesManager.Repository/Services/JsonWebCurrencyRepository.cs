using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QuotesManager.Interfaces;
using QuotesManager.Repository.Constants;
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

        public Task<CurrencyDto> GetCurrencyAsync(string id) => throw new NotImplementedException();

        public Task<IEnumerable<CurrencyDto>> FindCurrencyAsync(string filter) => throw new NotImplementedException();

        public Task<decimal> ConvertCurrencyAsync(string sourceCurrencyId, decimal sourceCurrencyNominal,
                                                  string targetCurrencyId) => throw new NotImplementedException();

        public async Task<IEnumerable<CurrencyPreviewDto>> GetCurrencyListAsync()
        {
            var result = new List<CurrencyPreviewDto>();
            var currencyMap = await LoadCurrencyListAsync();

            foreach (var keyValuePair in currencyMap)
            {
                var token = keyValuePair.Value;
                var id = keyValuePair.Key;
                var charCode = GetJTokenValueString(token, JsonKeys.CharCode);
                var numCode = GetJTokenValueString(token, JsonKeys.NumCode);

                var currencyPreview = new CurrencyPreviewDto
                {
                    CurrencyId = id,
                    CharCode = charCode,
                    NumCode = numCode
                };

                result.Add(currencyPreview);
            }

            return result;
        }

        private async Task<Dictionary<string, JToken>> LoadCurrencyListAsync()
        {
            using var webClient = new WebClient();
            var result = new Dictionary<string, JToken>();
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

                var id = GetJTokenValueString(currencyNode, JsonKeys.Id);

                result.Add(id, currencyNode);
            }

            return result;
        }

        private static string GetJTokenValueString(JToken token, string key) =>
            (token[key] ?? throw new NotSupportedException()).ToString();
    }
}