using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductsByUnitQuery : IRequest<APIResponseDto>
    {
        public Guid UnitId { get; }
        public GetProductsByUnitQuery(Guid unitId) => UnitId = unitId;
    }

}
