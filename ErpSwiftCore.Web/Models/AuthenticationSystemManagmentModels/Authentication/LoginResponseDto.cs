using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication
{

    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;
        public string Token { get; set; } = default!;
    }

}
