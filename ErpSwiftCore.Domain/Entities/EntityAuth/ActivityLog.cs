using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// سجل نشاط (Login، Logout، تغيير كلمة المرور … إلخ)
    /// </summary>
    public class ActivityLog : BaseEntity
    { 

        // معرف المستخدم الذي قام بالنشاط
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        // توقيت الحدث
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;



        // نوع النشاط (مثلاً: "Login", "PasswordChange", "TwoFactorVerified", ...)
        public string ActivityType { get; set; } = string.Empty;

        // عنوان الـ IP الذي تمت منه العملية
        public string? IpAddress { get; set; }

        // معلومات عن الجهاز المستخدَم (User-Agent مثلاً)
        public string? DeviceInfo { get; set; }
    }
}