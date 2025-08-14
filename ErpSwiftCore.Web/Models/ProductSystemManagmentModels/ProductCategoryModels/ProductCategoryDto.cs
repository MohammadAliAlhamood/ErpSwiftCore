using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels
{
    public class ProductCategoryDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ProductCategoryDto ParentCategory { get; set; }
     }


}
