using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// جلسة مسجلّة (Tokens أو Refresh Tokens يتم ربطها هنا إن احتجت لاحقاً)
    /// </summary>
    public class Session : BaseEntity
    { 
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
         
        public DateTime? ExpiresAt { get; set; }

        // معلومات عن الجهاز (للحالات المتعدد جلسات)
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
    }
}