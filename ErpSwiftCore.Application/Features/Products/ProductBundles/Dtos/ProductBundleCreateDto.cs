namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
{ 
    public class ProductBundleCreateDto
    {
        public Guid ParentProductId { get; set; }
        public Guid ComponentProductId { get; set; }
        public decimal Quantity { get; set; }
        public Guid? UnitOfMeasurementId { get; set; }
    } 
}