using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class RestoreProductUnitConversionCommand : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public RestoreProductUnitConversionCommand(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    }
}
