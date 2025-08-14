using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;
namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
 
        public Guid? CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}