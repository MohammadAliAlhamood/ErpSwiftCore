using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;

namespace ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels
{
    public class AuditableEntityDto : BaseEntityDto
    { 
        public CompanyDto? Company { get; set; }
    }
}