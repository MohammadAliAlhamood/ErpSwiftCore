using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompaniesByIndustryQuery : IRequest<APIResponseDto>
    {
        public string Industry { get; }

        public GetCompaniesByIndustryQuery(string industry)
        {
            Industry = industry;
        }
    }



}
