using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompaniesPagedByIndustryQuery : IRequest<APIResponseDto>
    {
        public string Industry { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompaniesPagedByIndustryQuery(string industry, int pageIndex, int pageSize)
        {
            Industry = industry;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

}
