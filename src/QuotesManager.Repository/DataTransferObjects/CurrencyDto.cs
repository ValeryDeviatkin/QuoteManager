namespace QuotesManager.Repository.DataTransferObjects
{
    public class CurrencyInfoDto
    {
        public string Id { get; set; }
        public string CharCode { get; set; }
        public string NumCode { get; set; }
        public string Name { get; set; }
        public CurrencyCourseDto[] Courses { get; set; }
    }
}