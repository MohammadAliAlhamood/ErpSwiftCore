using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.ValueObjectModels;

namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty; 
        public string TaxID { get; set; } = string.Empty;
        public IndustryType IndustryType { get; set; } = IndustryType.Unknown;
        public AddressDto? Address { get; set; }
        public ContactInfoDto ContactInfo { get; set; } = new();

    }

}
