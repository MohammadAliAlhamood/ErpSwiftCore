using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels
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