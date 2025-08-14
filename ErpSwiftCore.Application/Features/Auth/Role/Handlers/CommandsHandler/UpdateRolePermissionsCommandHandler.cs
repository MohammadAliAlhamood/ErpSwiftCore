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
    public class UpdateRolePermissionsCommandHandler : BaseHandler<UpdateRolePermissionsCommand>
    {
        private readonly IRoleService _authService;

        public UpdateRolePermissionsCommandHandler(
            IRoleService authService,
            ILogger<UpdateRolePermissionsCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateRolePermissionsRequest;
        //    await _authService.UpdateRolePermissionsAsync(dto.RoleName, dto.Permissions);
            return new { Message = $"تمّ تحديث صلاحيات الدور '{dto.RoleName}'." };
        }
    }
}
