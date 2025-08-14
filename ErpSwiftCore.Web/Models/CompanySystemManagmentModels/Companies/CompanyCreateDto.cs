
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.ValueObjectModels;

namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies
{
     
    public class CompanyCreateDto
    {
        public string CompanyName { get; set; } = string.Empty; 
        public string TaxID { get; set; } = string.Empty;
        public IndustryType IndustryType { get; set; } = IndustryType.Unknown;
        public ModifyAddressDto? Address { get; set; }
        public ModifyContactInfoDto ContactInfo { get; set; } = new();
     }
   
     
 
}
