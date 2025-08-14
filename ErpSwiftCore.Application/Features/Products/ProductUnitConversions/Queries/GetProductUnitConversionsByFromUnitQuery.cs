using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{

    public class GetProductUnitConversionsByFromUnitQuery : IRequest<APIResponseDto>
    {
        public Guid FromUnitId { get; }

        public GetProductUnitConversionsByFromUnitQuery(Guid fromUnitId)
        {
            FromUnitId = fromUnitId;
        }
    }

}
