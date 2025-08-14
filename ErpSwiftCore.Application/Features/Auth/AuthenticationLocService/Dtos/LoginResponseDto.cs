using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
