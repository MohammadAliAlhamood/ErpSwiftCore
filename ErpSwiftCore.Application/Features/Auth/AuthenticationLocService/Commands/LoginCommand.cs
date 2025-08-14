using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.Commands.Authentication
{
 
    public class LoginCommand : IRequest<APIResponseDto>
    {
        public LoginRequestDto LoginRequest { get; set; } = default!;
        public LoginCommand() { }
        public LoginCommand(LoginRequestDto loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }
}
