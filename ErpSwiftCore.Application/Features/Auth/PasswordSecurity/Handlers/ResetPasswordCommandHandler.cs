using ErpSwiftCore.Application.Features.Auth.Commands;
using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Handlers
{
    public class ResetPasswordCommandHandler : BaseHandler<ResetPasswordCommand>
    {
        private readonly IPasswordSecurityService _authService;
        private readonly IUserProfileService _userProfileService;
        public ResetPasswordCommandHandler(
            IPasswordSecurityService authService,
            IUserProfileService userProfileService,
            ILogger<ResetPasswordCommandHandler> logger
        ) : base(logger)
        {
            _userProfileService = userProfileService;
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ResetPasswordRequest;
            var existing = await _userProfileService.GetUserProfileAsync(dto.UserId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.UserId}' غير موجود.");

            await _authService.ResetPasswordAsync(dto.UserId, dto.NewPassword, dto.ResetToken);
            return new { Message = "تمّ إعادة تعيين كلمة المرور بنجاح." };
        }
    }
}
