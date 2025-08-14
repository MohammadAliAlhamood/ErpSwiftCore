using Microsoft.AspNetCore.Mvc.Rendering;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels;
namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductUnitConversionViewModel
    {
        public ProductUnitConversionDto ProductUnitConversion { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> FromUnitList { get; set; }
        public IEnumerable<SelectListItem> ToUnitList { get; set; }
    }
}
