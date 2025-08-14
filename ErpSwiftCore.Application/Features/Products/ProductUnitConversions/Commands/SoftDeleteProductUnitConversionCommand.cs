using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class SoftDeleteProductUnitConversionCommand : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public SoftDeleteProductUnitConversionCommand(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    }

}
