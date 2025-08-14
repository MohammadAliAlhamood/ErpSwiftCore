using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Session
{
    public class EndSessionRequestDto
    {
        public string SessionId { get; set; } = default!;
    }
}
