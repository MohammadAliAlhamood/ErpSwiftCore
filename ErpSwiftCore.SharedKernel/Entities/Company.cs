using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Enums;
using ErpSwiftCore.SharedKernel.ValueObjects;

namespace ErpSwiftCore.SharedKernel.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; } = string.Empty;
        public Address? Address { get; set; }
        public Contact? ContactInfo { get; set; }
        public IndustryType IndustryType { get; set; } = IndustryType.Unknown;
        public string? WebsiteURL { get; set; }
        public string? TaxID { get; set; }
        public string? LogoURL { get; set; }
        public string? Notes { get; set; }  
    }
}