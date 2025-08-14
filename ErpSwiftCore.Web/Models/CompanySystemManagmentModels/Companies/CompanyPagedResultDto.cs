using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies
{
    public class CompanyPagedResultDto
    {
        public IReadOnlyList<CompanyDto> Companies { get; set; } = new List<CompanyDto>();
        public int TotalCount { get; set; }
    }

}
