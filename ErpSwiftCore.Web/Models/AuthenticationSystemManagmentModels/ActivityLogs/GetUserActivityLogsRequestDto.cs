using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs
{

    /// <summary>
    /// طلب جلب سجلات نشاط مستخدم (Activity Logs) بين تاريخين محددين.
    /// </summary>
    public class GetUserActivityLogsRequestDto
    {
        public string UserId { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
