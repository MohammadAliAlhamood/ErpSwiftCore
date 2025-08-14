using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class ProductPrice : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product  Product { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency  Currency { get; set; }
        public ProductPriceType PriceType { get; set; }
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
    }
}