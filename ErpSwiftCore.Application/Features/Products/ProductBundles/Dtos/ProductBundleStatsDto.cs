namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
{
    public class ProductBundleStatsDto
    {
        public int TotalBundles { get; set; }
        public int ActiveBundles { get; set; }
        public int SoftDeletedBundles { get; set; }
        public int InactiveSoftDeletedBundles { get; set; }
    }
}
