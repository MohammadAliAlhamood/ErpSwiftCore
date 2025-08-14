using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class SearchCompaniesByKeywordQuery : IRequest<APIResponseDto>
    {
        public string Keyword { get; }

        public SearchCompaniesByKeywordQuery(string keyword)
        {
            Keyword = keyword;
        }
    }


}
