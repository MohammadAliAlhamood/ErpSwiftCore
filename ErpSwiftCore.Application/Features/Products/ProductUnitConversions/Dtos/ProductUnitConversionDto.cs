using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos
{
    public class ProductUnitConversionDto : AuditableEntityDto
    {
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Guid FromUnitId { get; set; }
        public UnitOfMeasurementDto FromUnit { get; set; }
        public Guid ToUnitId { get; set; }
        public UnitOfMeasurementDto ToUnit { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Factor { get; set; }
    }
}