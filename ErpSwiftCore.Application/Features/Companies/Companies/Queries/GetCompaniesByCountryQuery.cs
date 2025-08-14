using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompaniesByCountryQuery : IRequest<APIResponseDto>
    {
        public string Country { get; }

        public GetCompaniesByCountryQuery(string country)
        {
            Country = country;
        }
    }
}
