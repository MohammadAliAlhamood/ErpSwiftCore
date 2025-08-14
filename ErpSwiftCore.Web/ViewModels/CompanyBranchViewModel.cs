using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSwiftCore.Web.ViewModels
{
    public class CompanyBranchViewModel
    {
        public CompanyBranchDto CompanyBranch { get; set; }
        public IEnumerable<SelectListItem> CompanyList { get; set; }
 
    }
}
