using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs
{
    /// <summary>
    /// تمثيل لجدول ActivityLog في الرد (Response).
    /// </summary>
    public class ActivityLogDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public DateTimeOffset Timestamp { get; set; }

        public string ActivityType { get; set; } = string.Empty;

        // عنوان الـ IP الذي تمت منه العملية
        public string? IpAddress { get; set; }

        // معلومات عن الجهاز المستخدَم (User-Agent مثلاً)
        public string? DeviceInfo { get; set; }
    }

}
