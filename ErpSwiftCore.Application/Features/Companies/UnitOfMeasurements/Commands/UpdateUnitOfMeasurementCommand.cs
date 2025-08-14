using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands
{
    public class UpdateUnitOfMeasurementCommand : IRequest<APIResponseDto>
    {
        public UnitOfMeasurementUpdateDto Unit { get; }

        public UpdateUnitOfMeasurementCommand(UnitOfMeasurementUpdateDto unit)
        {
            Unit = unit;
        }
    }
}
