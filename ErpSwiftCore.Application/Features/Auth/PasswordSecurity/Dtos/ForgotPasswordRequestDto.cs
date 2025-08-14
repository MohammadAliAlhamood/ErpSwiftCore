namespace ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity
{
    /// <summary>
    /// طلب إنشاء رمز إعادة تعيين كلمة المرور (Forgot Password).
    /// </summary>
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = default!;
    }

}
