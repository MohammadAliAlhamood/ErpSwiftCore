using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using MediatR;
namespace ErpSwiftCore.Application.Features.Auth.Commands.UserProfile
{
    /// <summary>
    /// أمر تسجيل مستخدم جديد.
    /// يتضمّن جميع البيانات المطلوبة في RegisterRequestDto.
    /// </summary>
    public class RegisterCommand : IRequest<APIResponseDto>
    {
        public RegisterRequestDto RegisterRequest { get; set; } = default!;
        public RegisterCommand() { }
        public RegisterCommand(RegisterRequestDto registerRequest)
        {
            RegisterRequest = registerRequest;
        }
    } 
}