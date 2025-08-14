using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.ValueObjects;

namespace ErpSwiftCore.SharedKernel.Entities
{
    public class CompanyBranch : BaseEntity
    {
        public Guid CompanyID { get; set; }
        public Company? Company { get; set; }
        public string? BranchName { get; set; }
        public Address? Address { get; set; }
        public Contact? ContactInfo { get; set; } 
        public string BranchCode { get; set; }
    }
}