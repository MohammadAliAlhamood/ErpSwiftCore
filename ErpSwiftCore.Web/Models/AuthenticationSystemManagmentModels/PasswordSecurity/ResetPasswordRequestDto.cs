using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.PasswordSecurity
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
