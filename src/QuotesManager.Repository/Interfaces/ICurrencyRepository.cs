using System.Collections.Generic;
using System.Threading.Tasks;
using QuotesManager.Repository.DataTransferObjects;

namespace QuotesManager.Repository.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<CurrencyPreviewDto>> GetCurrencyListAsync();
        Task<CurrencyInfoDto> GetCurrencyAsync(string id);
        Task<IEnumerable<CurrencyInfoDto>> FindCurrencyAsync(string filter);

        Task<decimal> ConvertCurrencyAsync(string sourceCurrencyId, decimal sourceCurrencyNominal,
                                           string targetCurrencyId);
    }
}