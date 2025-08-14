using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionsByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }

        public GetProductUnitConversionsByProductQuery(Guid productId)
        {
            ProductId = productId;
        }
    }





}
