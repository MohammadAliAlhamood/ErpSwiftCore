using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels
{
    public class ProductPriceDto : AuditableEntityDto
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; }
        public ProductPriceType PriceType { get; set; }
        public DateTime EffectiveDate { get; set; }
    } 
}
