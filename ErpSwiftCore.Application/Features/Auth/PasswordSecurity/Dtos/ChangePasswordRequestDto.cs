namespace ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity
{
    public class ChangePasswordRequestDto
    { 
        public string CurrentPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
