using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class ProductTax : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product  Product { get; set; }
        public decimal Rate { get; set; } 
    }
}