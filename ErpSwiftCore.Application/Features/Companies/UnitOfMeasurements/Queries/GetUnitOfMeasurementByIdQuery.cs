using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Queries
{
    public class GetUnitOfMeasurementByIdQuery : IRequest<APIResponseDto>
    {
        public Guid UnitId { get; }

        public GetUnitOfMeasurementByIdQuery(Guid unitId)
        {
            UnitId = unitId;
        }
    }
}
