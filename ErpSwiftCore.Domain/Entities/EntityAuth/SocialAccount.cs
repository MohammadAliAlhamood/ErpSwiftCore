using ErpSwiftCore.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.Entities.EntityAuth
{
    /// <summary>
    /// ربط حسابات الـ Social Login (مثل Google, Facebook … إلخ)
    /// </summary>
    public class SocialAccount : BaseEntity
    { 

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        // اسم مقدم الخدمة (مثل "Google", "Facebook")
        public string Provider { get; set; } = string.Empty;

        // معرف المستخدم في مزود الخدمة الخارجي
        public string ProviderUserId { get; set; } = string.Empty;
    }
}