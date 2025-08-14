using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role
{
    public class AssignRoleRequestDto
    {
        public string UserId { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
