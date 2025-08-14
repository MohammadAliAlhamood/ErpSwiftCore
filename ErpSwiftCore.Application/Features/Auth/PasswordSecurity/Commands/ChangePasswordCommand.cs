using ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands
{

    /// <summary>
    /// أمر تغيير كلمة المرور.
    /// يتضمّن ChangePasswordRequestDto (UserId, CurrentPassword, NewPassword).
    /// </summary>
    public class ChangePasswordCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات تغيير كلمة المرور.
        /// </summary>
        public ChangePasswordRequestDto ChangePasswordRequest { get; set; } = default!;

        public ChangePasswordCommand() { }

        public ChangePasswordCommand(ChangePasswordRequestDto changePasswordRequest)
        {
            ChangePasswordRequest = changePasswordRequest;
        }
    }
}
