using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ProductIds { get; }
        public GetProductsByIdsQuery(IEnumerable<Guid> productIds) => ProductIds = productIds;
    }

}
