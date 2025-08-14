using ErpSwiftCore.Application.Features.Auth.Commands;
using ErpSwiftCore.Application.Features.Auth.Role.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Role.Handlers.CommandsHandler
{
    public class AssignRoleCommandHandler : BaseHandler<AssignRoleCommand>
    {
        private readonly IUserProfileService _authService;
        private readonly IRoleService _roleService;
        public AssignRoleCommandHandler(
            IUserProfileService authService,
            IRoleService roleService,
            ILogger<AssignRoleCommandHandler> logger
        ) : base(logger)
        {
            _roleService = roleService;
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AssignRoleRequest;
            var existing = await _authService.GetUserProfileAsync(dto.UserId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.UserId}' غير موجود.");

            await _roleService.AssignRoleAsync(dto.UserId, dto.RoleName);
            return new { Message = $"تمّ تعيين الدور '{dto.RoleName}' للمستخدم '{dto.UserId}'." };
        }
    }
}
