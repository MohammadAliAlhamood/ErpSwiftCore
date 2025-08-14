using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;

namespace ErpSwiftCore.Web.ViewModels
{
    public class BulkCreateCompaniesViewModel
    {
        public IEnumerable<CompanyCreateDto> Companies { get; set; } = new List<CompanyCreateDto>();
    }

}
