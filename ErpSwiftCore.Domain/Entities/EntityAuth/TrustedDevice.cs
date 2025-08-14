using ErpSwiftCore.SharedKernel.Base;
using System.ComponentModel.DataAnnotations;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// أجهزة موثوقة (مثلاً جهازي الكمبيوتر والجوال) المرتبطة بالمستخدم لعدم المطالبة بإعادة التحقق
    /// </summary>
    public class TrustedDevice : BaseEntity
    { 

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public string DeviceName { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}