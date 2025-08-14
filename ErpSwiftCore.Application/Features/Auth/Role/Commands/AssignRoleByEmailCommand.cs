using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using MediatR; 

namespace ErpSwiftCore.Application.Features.Auth.Role.Commands
{
    /// <summary>
    /// Command لتعيين دور اعتمادًا على البريد الإلكتروني.
    /// </summary>
    public class AssignRoleByEmailCommand : IRequest<APIResponseDto>
    {
        public AssignRoleByEmailRequestDto AssignRoleRequest { get; set; } = default!;

        public AssignRoleByEmailCommand(AssignRoleByEmailRequestDto dto)
        {
            AssignRoleRequest = dto;
        }
    }
}
