using Microsoft.Extensions.Logging;
using MediatR;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Application.Features.Auth.Role.Commands;

namespace ErpSwiftCore.Application.Features.Auth.Role.Handlers.CommandsHandler
{ 
    public class AssignRoleByEmailCommandHandler : BaseHandler<AssignRoleByEmailCommand>
    {
        private readonly IRoleService _authService;

        public AssignRoleByEmailCommandHandler(
            IRoleService authService,
            ILogger<AssignRoleByEmailCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(
            AssignRoleByEmailCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AssignRoleRequest;

            // نفّذ التعيين
            await _authService.AssignRoleByEmailAsync(dto.Email, dto.RoleName);

            return new
            {
                Message = $"تمّ تعيين الدور '{dto.RoleName}' للمستخدم صاحب البريد '{dto.Email}'."
            };
        }
    } 
}
