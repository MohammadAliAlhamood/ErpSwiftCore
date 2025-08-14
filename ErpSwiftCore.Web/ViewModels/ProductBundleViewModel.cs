using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductBundleViewModel
    {
        public ProductBundleDto ProductBundle { get; set; }
        public IEnumerable<SelectListItem> UnitOfMeasurementList { get; set; }
        public IEnumerable<SelectListItem> ParentProductList { get; set; }
        public IEnumerable<SelectListItem> ComponentProductList { get; set; }

    }
}
