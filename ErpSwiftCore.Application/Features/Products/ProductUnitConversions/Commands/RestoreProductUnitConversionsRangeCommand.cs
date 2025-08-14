using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class RestoreProductUnitConversionsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ConversionIds { get; }

        public RestoreProductUnitConversionsRangeCommand(IEnumerable<Guid> conversionIds)
        {
            ConversionIds = conversionIds;
        }
    }


}
