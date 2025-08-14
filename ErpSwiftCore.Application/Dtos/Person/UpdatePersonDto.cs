using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Application.Dtos.Person
{
    /// <summary>
    /// بيانات تحديث كيان مشتق من Person
    /// </summary>
    public class UpdatePersonDto
    {
        public Guid ID { get; set; }
        public string? Notes { get; set; }
        public GenderType Gender { get; set; }
        public string NationalID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public ModifyAddressDto Address { get; set; } = null!;
        public ModifyContactInfoDto ContactInfo { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}