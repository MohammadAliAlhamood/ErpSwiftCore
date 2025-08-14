using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ErpSwiftCore.Web.ViewModels
{
    public class ProductPriceViewModel
    {
        public ProductPriceDto ProductPrice { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> CurrencyList { get; set; }

    }
}
