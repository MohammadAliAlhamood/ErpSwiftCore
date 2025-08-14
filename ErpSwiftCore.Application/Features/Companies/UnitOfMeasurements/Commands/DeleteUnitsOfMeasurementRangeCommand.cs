using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands
{
    public class DeleteUnitsOfMeasurementRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> UnitIds { get; }

        public DeleteUnitsOfMeasurementRangeCommand(IEnumerable<Guid> unitIds)
        {
            UnitIds = unitIds;
        }
    }
}
