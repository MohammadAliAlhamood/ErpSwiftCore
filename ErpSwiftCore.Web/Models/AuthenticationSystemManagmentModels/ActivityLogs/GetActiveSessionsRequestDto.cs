using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs
{


    /// <summary>
    /// لا توجد حاجة لطلب (Request) خاص لأن الدالة تأخذ userId فقط كـ path parameter،
    /// لكن أدرجت هذا الـDTO للاتساق (يمكن تجاهله إذا يتم استخدام route مباشرة).
    /// </summary>
    public class GetActiveSessionsRequestDto
    {
        public string UserId { get; set; } = default!;
    }
}
