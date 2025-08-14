using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.PasswordSecurity
{
    /// <summary>
    /// طلب إنشاء رمز إعادة تعيين كلمة المرور (Forgot Password).
    /// </summary>
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = default!;
    }

}
