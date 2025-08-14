using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class BulkSoftDeleteProductUnitConversionsCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ConversionIds { get; }

        public BulkSoftDeleteProductUnitConversionsCommand(IEnumerable<Guid> conversionIds)
        {
            ConversionIds = conversionIds;
        }
    }
}
