using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityProduct
{
    public class ProductCategory : AuditableEntity
    {
        public string? Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ProductCategory  ParentCategory { get; set; }
    }
}