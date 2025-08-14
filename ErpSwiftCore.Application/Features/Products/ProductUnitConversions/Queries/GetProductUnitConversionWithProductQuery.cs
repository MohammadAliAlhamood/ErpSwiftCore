using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public GetProductUnitConversionWithProductQuery(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    }

}
