using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Dtos.ValueObjectDto
{
    public class ModifyContactInfoDto
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        public string? Other { get; set; }
    }


}
