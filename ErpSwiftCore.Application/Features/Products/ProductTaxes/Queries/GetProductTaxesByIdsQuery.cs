using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class GetProductTaxesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> TaxIds { get; }

        public GetProductTaxesByIdsQuery(IEnumerable<Guid> taxIds)
        {
            TaxIds = taxIds;
        }
    }


}
