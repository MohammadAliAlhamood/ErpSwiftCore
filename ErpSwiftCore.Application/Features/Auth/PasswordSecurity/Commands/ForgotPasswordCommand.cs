using ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity;
using MediatR;
namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands
{

    /// <summary>
    /// أمر نسيان كلمة المرور (إنشاء رمز إعادة التعيين).
    /// يتضمّن ForgotPasswordRequestDto (Email).
    /// </summary>
    public class ForgotPasswordCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات نسيان كلمة المرور.
        /// </summary>
        public ForgotPasswordRequestDto ForgotPasswordRequest { get; set; } = default!;

        public ForgotPasswordCommand() { }

        public ForgotPasswordCommand(ForgotPasswordRequestDto forgotPasswordRequest)
        {
            ForgotPasswordRequest = forgotPasswordRequest;
        }
    }

}
