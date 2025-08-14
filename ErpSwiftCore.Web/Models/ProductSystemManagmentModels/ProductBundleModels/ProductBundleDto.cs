using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels
{
    public class ProductBundleDto : AuditableEntityDto
    {
        public Guid ParentProductId { get; set; }
        public ProductDto? ParentProduct { get; set; }

        public Guid ComponentProductId { get; set; }
        public ProductDto? ComponentProduct { get; set; }

        public decimal Quantity { get; set; }

        public Guid? UnitOfMeasurementId { get; set; }
        public UnitOfMeasurementDto? UnitOfMeasurement { get; set; }
    }
}
