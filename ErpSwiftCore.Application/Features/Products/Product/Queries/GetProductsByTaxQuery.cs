using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByTaxQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }
        public GetProductsByTaxQuery(Guid taxId) => TaxId = taxId;
    }

}
