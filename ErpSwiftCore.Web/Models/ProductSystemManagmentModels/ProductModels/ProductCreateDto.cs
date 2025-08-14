using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
