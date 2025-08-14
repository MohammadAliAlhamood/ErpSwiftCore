using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class ProductUnitConversion : AuditableEntity
    {
        public Guid ProductId { get; set; }
        public Product  Product { get; set; }
        public Guid FromUnitId { get; set; }
        public UnitOfMeasurement  FromUnit { get; set; }
        public Guid ToUnitId { get; set; }
        public UnitOfMeasurement  ToUnit { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Factor { get; set; } 
    }
}