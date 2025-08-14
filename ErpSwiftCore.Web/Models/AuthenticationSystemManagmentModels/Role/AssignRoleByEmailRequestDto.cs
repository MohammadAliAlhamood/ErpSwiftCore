using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role
{
    /// <summary>
    /// بيانات الطلب لتعيين دور للمستخدم اعتمادًا على البريد الإلكتروني.
    /// </summary>
    public class AssignRoleByEmailRequestDto
    {
        /// <summary>
        /// البريد الإلكتروني للمستخدم.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// اسم الدور المراد تعيينه للمستخدم.
        /// </summary>
        public string RoleName { get; set; } = default!;
    }
}
