using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class SearchCompaniesByNameQuery : IRequest<APIResponseDto>
    {
        public string Name { get; }

        public SearchCompaniesByNameQuery(string name)
        {
            Name = name;
        }
    }


}
