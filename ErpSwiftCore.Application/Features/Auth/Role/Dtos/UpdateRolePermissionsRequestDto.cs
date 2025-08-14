using System.ComponentModel.DataAnnotations; 
namespace ErpSwiftCore.Application.Features.Auth.Role.Dtos
{
    public class UpdateRolePermissionsRequestDto
    {
        [Required]
        public string RoleName { get; set; } = default!;
        public IEnumerable<string> Permissions { get; set; } = new List<string>();
    }
}
