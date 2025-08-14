using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserManagement
{
    /// <summary>
    /// طلب حظر مستخدم.
    /// </summary>
    public class BlockUserRequestDto
    {
        public string UserId { get; set; } = default!;
    }
}
