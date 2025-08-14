using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryDto ProductCategory { get; set; }
        public IEnumerable<SelectListItem> ParentCategoryList { get; set; }

    }
}
