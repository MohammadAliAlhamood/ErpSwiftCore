using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.Role.Commands
{


    /// <summary>
    /// أمر تحديث صلاحيات دور معيّن.
    /// يتضمّن UpdateRolePermissionsRequestDto (RoleName, Permissions).
    /// </summary>
    public class UpdateRolePermissionsCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات تحديث صلاحيات الدور (RoleName, Permissions).
        /// </summary>
        public UpdateRolePermissionsRequestDto UpdateRolePermissionsRequest { get; set; } = default!;

        public UpdateRolePermissionsCommand() { }

        public UpdateRolePermissionsCommand(UpdateRolePermissionsRequestDto updateRolePermissionsRequest)
        {
            UpdateRolePermissionsRequest = updateRolePermissionsRequest;
        }
    }
}
