using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// تنبيه أمني يُرسل للمستخدم (مثل: محاولة تسجيل من موقع غريب … إلخ)
    /// </summary>
    public class SecurityAlert : BaseEntity
    { 

        // المستخدم الذي سيصله التنبيه
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // نص الرسالة
        public string? Message { get; set; }

        // الوقت الذي أُنشئ فيه التنبيه
        public DateTime AlertTime { get; set; } = DateTime.UtcNow;
    }
}