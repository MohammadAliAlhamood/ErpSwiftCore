using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Dtos
{
    public class CompanyPagedResultDto
    {
        public IReadOnlyList<CompanyDto> Companies { get; set; } = new List<CompanyDto>();
        public int TotalCount { get; set; }
    }

}
