using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserManagement
{
    /// <summary>
    /// طلب حذف مستخدم نهائيًا.
    /// </summary>
    public class DeleteUserRequestDto
    {
        public string UserId { get; set; } = default!;
    }
}
