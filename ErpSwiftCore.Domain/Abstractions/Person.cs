using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.ValueObjects;

namespace ErpSwiftCore.Domain.Abstractions
{
    /// <summary>
    /// Abstract person entity. Inherits TenantID from AuditableEntity.
    /// TenantID must always reference Company.ID (the main tenant).
    /// </summary>
    public abstract class Person : AuditableEntity
    {
        public string? Notes { get; set; }
        public GenderType Gender { get; set; }
        public string NationalID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public Address  Address { get; set; }
        public Contact  ContactInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}