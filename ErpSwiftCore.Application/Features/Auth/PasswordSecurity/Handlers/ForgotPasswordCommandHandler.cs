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
    public class ForgotPasswordCommandHandler : BaseHandler<ForgotPasswordCommand>
    {
        private readonly IUserProfileService _authService;
        private readonly IPasswordSecurityService _passwordSecurityService;
        public ForgotPasswordCommandHandler(
            IUserProfileService authService,
            IPasswordSecurityService passwordSecurityService,
            ILogger<ForgotPasswordCommandHandler> logger
        ) : base(logger)
        {
            _passwordSecurityService = passwordSecurityService;
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ForgotPasswordRequest;
            var user = await _authService.GetUserProfileAsync(dto.Email);
            if (user == null) throw new DomainNotFoundException($"لا يوجد مستخدم مسجل بالعنوان '{dto.Email}'.");
            await _passwordSecurityService.ForgotPasswordAsync(dto.Email);
            return new { Message = "تمّ إرسال رابط إعادة التعيين إلى بريدك الإلكتروني." };
        }
    }
}
