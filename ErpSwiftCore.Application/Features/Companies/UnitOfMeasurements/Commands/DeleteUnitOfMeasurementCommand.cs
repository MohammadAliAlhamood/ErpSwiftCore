using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands
{
    public class DeleteUnitOfMeasurementCommand : IRequest<APIResponseDto>
    {
        public Guid UnitId { get; }

        public DeleteUnitOfMeasurementCommand(Guid unitId)
        {
            UnitId = unitId;
        }
    }
}
