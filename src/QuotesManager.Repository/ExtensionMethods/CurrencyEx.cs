using QuotesManager.Repository.DataModels;

namespace QuotesManager.Repository.ExtensionMethods
{
    internal static class CurrencyEx
    {
        public static decimal GetUnitValue(this CurrencyDataModel currency) => currency.Value / currency.Nominal;
    }
}