using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// يرث من IdentityUser لضبط الحقول الأساسية للمستخدم.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // اسم أو لقب المستخدم الكامل
        public string? Name { get; set; }

        // معرف الشركة (التينانت) للمستخدم
        private Guid? tenantID;
        public Guid? GetTenantID() => tenantID;
        public void SetTenantID(Guid? value) => tenantID = value;

        // عنوان الصورة الشخصيّة
        public string? ProfilePictureUrl { get; set; }

        // عنوان سكني أو تجاري
        public string? Address { get; set; }

        // هل المستخدم مشترك في إشعارات الأمان؟
        public bool IsSubscribedToSecurityNotifications { get; set; }

       
        // الربط إلى الكيان Company إن وجد
        public Guid? TenantId
        {
            get => GetTenantID();
            set => SetTenantID(value);
        }
        public bool IsBlocked =>
            LockoutEnabled && LockoutEnd != null &&
            LockoutEnd > DateTimeOffset.UtcNow;

        [ForeignKey(nameof(TenantId))]
        public Company? Company { get; set; }  // نفترض وجود كلاس Company في SharedKernel أو مشروع منفصل

        // علاقات واحد إلى كثير مع entities أخرى
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
        public ICollection<Session>? Sessions { get; set; }
        public ICollection<SecurityAlert>? SecurityAlerts { get; set; }
        public ICollection<TrustedDevice>? TrustedDevices { get; set; }
        public ICollection<SocialAccount>? SocialAccounts { get; set; }
    }
}