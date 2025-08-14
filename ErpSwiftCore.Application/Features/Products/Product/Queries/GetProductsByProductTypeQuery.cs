using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByProductTypeQuery : IRequest<APIResponseDto>
    {
        public Guid ProductTypeId { get; }
        public GetProductsByProductTypeQuery(Guid productTypeId) => ProductTypeId = productTypeId;
    }


}
