using Microsoft.AspNetCore.Mvc.Rendering;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels;
namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductTaxViewModel
    {
        public ProductTaxDto ProductTax { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
