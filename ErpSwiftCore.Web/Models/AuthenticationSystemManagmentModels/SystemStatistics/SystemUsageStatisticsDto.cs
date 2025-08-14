using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.SystemStatistics
{
    /// <summary>
    /// إحصائيات عامة عن النظام (عدد المستخدمين، النشطين، المحظورين، الأدوار).
    /// </summary>
    public class SystemUsageStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int BlockedUsers { get; set; }
        public int TotalRoles { get; set; }
    }
}
