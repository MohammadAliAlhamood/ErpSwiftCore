using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role
{


    public class UpdateRolePermissionsRequestDto
    {
        [Required]
        public string RoleName { get; set; } = default!;
        public IEnumerable<string> Permissions { get; set; } = new List<string>();
    }
}
