using ErpSwiftCore.Application.Features.Auth.Commands;
using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Handlers
{
    public class ChangePasswordCommandHandler : BaseHandler<ChangePasswordCommand>
    {
        private readonly IUserProfileService _authService;
        private readonly IPasswordSecurityService _passwordSecurityService;
        private readonly IUserProvider _userProvider;
        public ChangePasswordCommandHandler(
            IUserProfileService authService,
            IUserProvider userProvider,

            IPasswordSecurityService passwordSecurityService,
            ILogger<ChangePasswordCommandHandler> logger
        ) : base(logger)
        {
            _passwordSecurityService = passwordSecurityService;
            _authService = authService;
            _userProvider = userProvider;

        }

        protected override async Task<object?> HandleRequestAsync(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _userProvider.GetUserId().ToString();

            var dto = request.ChangePasswordRequest;

            await _passwordSecurityService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            return new { Message = "تمّ تغيير كلمة المرور بنجاح." };
        }
    }
}
