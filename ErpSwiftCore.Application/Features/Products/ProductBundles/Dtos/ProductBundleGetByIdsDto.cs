namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
{
    public class ProductBundleGetByIdsDto
    {
        public IEnumerable<Guid> BundleIds { get; set; } = new List<Guid>();
    }
}
