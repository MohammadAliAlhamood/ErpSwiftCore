namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
{
    public class ProductBundleBulkImportResultDto
    {
        public int ImportedCount { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
