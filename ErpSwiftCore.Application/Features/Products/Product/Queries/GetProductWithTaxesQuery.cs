using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductWithTaxesQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductWithTaxesQuery(Guid productId) => ProductId = productId;
    }


}
