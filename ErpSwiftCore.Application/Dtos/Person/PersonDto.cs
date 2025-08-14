using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Application.Dtos.Person
{
    /// <summary>
    /// DTO لتمثيل البيانات المشتركة بين جميع الكيانات المشتقة من Person
    /// </summary>
    public class PersonDto : AuditableEntityDto
    {
        public string? Notes { get; set; }
        public GenderType Gender { get; set; }
        public string NationalID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = null!;
        public ContactInfoDto ContactInfo { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
