namespace QuotesManager.Repository.DataModels
{
    internal class CurrencyDataModel
    {
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}