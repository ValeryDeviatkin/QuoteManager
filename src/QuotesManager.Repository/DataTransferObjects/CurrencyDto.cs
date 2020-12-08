namespace QuotesManager.Repository.DataTransferObjects
{
    public class CurrencyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public CurrencyCourseDto[] Values { get; set; }
    }
}