using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.SharedKernel.Enums; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Dtos
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
