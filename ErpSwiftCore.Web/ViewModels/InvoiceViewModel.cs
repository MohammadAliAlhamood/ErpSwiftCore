using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using Microsoft.AspNetCore.Mvc.Rendering; 
namespace ErpSwiftCore.Web.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceDto Invoice { get; set; }
        public IEnumerable<SelectListItem> OrderList { get; set; }
        public IEnumerable<SelectListItem> CurrencyList { get; set; }

        // هذه الخاصية تجمع IDs البنود المحذوفة في الواجهة
        public IEnumerable<Guid> LinesDeletedIds { get; set; } = new List<Guid>();
    } 
}
