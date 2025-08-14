using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductViewModel
    {
        public ProductDto Product { get; set; }
        public IEnumerable<SelectListItem> UnitOfMeasurementList { get; set; } 
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}
