using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
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
