using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ErpSwiftCore.Web.ViewModels
{
    public class OrderViewModel
    {
        public OrderDto Order { get; set; }
        public IEnumerable<SelectListItem> CurrencyList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }

        // لتجميع معرفات السطور التي يطلب المستخدم حذفها من الواجهة
        public List<Guid> DeletedLineIds { get; set; } = new List<Guid>();
    }
}