namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels
{
    public class ProductCategoryCreateDto  
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }

}
