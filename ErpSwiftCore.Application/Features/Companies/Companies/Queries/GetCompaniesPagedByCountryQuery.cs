using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompaniesPagedByCountryQuery : IRequest<APIResponseDto>
    {
        public string Country { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompaniesPagedByCountryQuery(string country, int pageIndex, int pageSize)
        {
            Country = country;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

}
