using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels
{
    public class ProductPriceCreateDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public ProductPriceType PriceType { get; set; }
        public DateTime EffectiveDate { get; set; }
    }


}
