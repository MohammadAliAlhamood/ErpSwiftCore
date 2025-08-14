namespace ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity
{

    /// <summary>
    /// طلب إعادة تعيين كلمة المرور (Reset Password) باستخدام التوكن المرسل عبر البريد.
    /// </summary>
    public class ResetPasswordRequestDto
    {
        public string UserId { get; set; } = default!;
        public string ResetToken { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
