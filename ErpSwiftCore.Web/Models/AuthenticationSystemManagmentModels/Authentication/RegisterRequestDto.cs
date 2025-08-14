using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication
{
    /// <summary>
    /// بيانات طلب تسجيل مستخدم جديد (Register).
    /// </summary>
    public class RegisterRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? UserName { get; set; }
        public string Name { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Address { get; set; }
        public string? RoleName { get; set; }
    }
}
