using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    // Include related Product
    public class GetProductTaxWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }

        public GetProductTaxWithProductQuery(Guid taxId)
        {
            TaxId = taxId;
        }
    }

}
