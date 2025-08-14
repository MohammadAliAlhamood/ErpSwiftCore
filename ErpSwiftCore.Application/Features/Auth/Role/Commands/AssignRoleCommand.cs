using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.Role.Commands
{

    /// <summary>
    /// أمر تعيين دور لمستخدم.
    /// يتضمّن AssignRoleRequestDto (UserId, RoleName).
    /// </summary>
    public class AssignRoleCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات تعيين الدور (UserId, RoleName).
        /// </summary>
        public AssignRoleRequestDto AssignRoleRequest { get; set; } = default!;

        public AssignRoleCommand() { }

        public AssignRoleCommand(AssignRoleRequestDto assignRoleRequest)
        {
            AssignRoleRequest = assignRoleRequest;
        }
    }
}
