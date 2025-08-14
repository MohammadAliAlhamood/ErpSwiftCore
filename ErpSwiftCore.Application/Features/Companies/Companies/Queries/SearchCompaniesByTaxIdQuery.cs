using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class SearchCompaniesByTaxIdQuery : IRequest<APIResponseDto>
    {
        public string TaxId { get; }

        public SearchCompaniesByTaxIdQuery(string taxId)
        {
            TaxId = taxId;
        }
    }
}
