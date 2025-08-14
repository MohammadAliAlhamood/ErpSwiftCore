using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetSoftDeletedProductUnitConversionByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public GetSoftDeletedProductUnitConversionByIdQuery(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    }
}
