using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompanyByNameQuery : IRequest<APIResponseDto>
    {
        public string CompanyName { get; }

        public GetCompanyByNameQuery(string companyName)
        {
            CompanyName = companyName;
        }
    }
    }





