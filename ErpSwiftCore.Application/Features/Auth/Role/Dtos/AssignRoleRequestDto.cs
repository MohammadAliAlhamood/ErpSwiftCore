namespace ErpSwiftCore.Application.Features.Auth.Role.Dtos
{
    public class AssignRoleRequestDto
    {
        public string UserId { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
