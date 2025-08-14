using ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity; 
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands
{
    /// <summary>
    /// أمر إعادة تعيين كلمة المرور باستخدام التوكن.
    /// يتضمّن ResetPasswordRequestDto (UserId, ResetToken, NewPassword).
    /// </summary>
    public class ResetPasswordCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات إعادة التعيين.
        /// </summary>
        public ResetPasswordRequestDto ResetPasswordRequest { get; set; } = default!;

        public ResetPasswordCommand() { }

        public ResetPasswordCommand(ResetPasswordRequestDto resetPasswordRequest)
        {
            ResetPasswordRequest = resetPasswordRequest;
        }
    }

}
