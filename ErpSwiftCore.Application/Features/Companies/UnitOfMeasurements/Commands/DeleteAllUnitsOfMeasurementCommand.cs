using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands
{
    public class DeleteAllUnitsOfMeasurementCommand : IRequest<APIResponseDto>
    {
        public DeleteAllUnitsOfMeasurementCommand() { }
    }
}