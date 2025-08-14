using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication
{

    public class LoginRequestDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
