using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{

    public class GetProductUnitConversionWithFromUnitQuery : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public GetProductUnitConversionWithFromUnitQuery(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    }

}
