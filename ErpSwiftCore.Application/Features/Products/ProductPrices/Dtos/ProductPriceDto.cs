using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos
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
