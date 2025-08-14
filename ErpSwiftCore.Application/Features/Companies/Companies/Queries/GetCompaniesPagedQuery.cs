using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{

    public class GetCompaniesPagedQuery : IRequest<APIResponseDto>
    {
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompaniesPagedQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

}
