using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompanyByCodeQuery : IRequest<APIResponseDto>
    {
        public string CompanyCode { get; }

        public GetCompanyByCodeQuery(string companyCode)
        {
            CompanyCode = companyCode;
        }
    }



}
