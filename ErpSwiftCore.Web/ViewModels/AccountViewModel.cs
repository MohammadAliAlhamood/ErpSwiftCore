using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSwiftCore.Web.ViewModels
{ 
    public class AccountViewModel
    {
        public AccountDto Account { get; set; } 
        public IEnumerable<SelectListItem> CurrencyList { get; set; }

    }
}
