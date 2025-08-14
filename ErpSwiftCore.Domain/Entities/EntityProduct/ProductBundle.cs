using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class ProductBundle : AuditableEntity
    {


        public Guid ParentProductId { get; set; }
        public Product  ParentProduct { get; set; } 
        public Guid ComponentProductId { get; set; }
        public Product ComponentProduct { get; set; }
        public decimal Quantity { get; set; }
        public Guid? UnitOfMeasurementId { get; set; }
        public UnitOfMeasurement  UnitOfMeasurement { get; set; } 
    }
}