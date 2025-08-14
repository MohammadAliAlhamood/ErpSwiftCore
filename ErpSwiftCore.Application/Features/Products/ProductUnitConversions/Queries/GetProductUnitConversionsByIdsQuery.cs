using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{

    public class GetProductUnitConversionsByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ConversionIds { get; }

        public GetProductUnitConversionsByIdsQuery(IEnumerable<Guid> conversionIds)
        {
            ConversionIds = conversionIds;
        }
    }

}
