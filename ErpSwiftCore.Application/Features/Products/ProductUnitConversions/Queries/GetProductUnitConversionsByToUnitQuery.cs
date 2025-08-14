using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionsByToUnitQuery : IRequest<APIResponseDto>
    {
        public Guid ToUnitId { get; }

        public GetProductUnitConversionsByToUnitQuery(Guid toUnitId)
        {
            ToUnitId = toUnitId;
        }
    }


}
