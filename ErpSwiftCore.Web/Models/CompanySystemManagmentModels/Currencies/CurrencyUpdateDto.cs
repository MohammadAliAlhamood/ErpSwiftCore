namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies
{
    public class CurrencyUpdateDto
    {
        public Guid ID { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
    }
}