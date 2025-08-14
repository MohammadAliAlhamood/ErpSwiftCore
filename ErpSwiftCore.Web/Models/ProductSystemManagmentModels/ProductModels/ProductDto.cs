using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels
{
    public class ProductDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public UnitOfMeasurementDto UnitOfMeasurement { get; set; }
 
        public Guid? CategoryId { get; set; }
        public ProductCategoryDto Category  { get; set; }
    }
}