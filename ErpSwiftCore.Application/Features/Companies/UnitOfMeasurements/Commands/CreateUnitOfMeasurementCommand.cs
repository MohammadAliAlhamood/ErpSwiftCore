using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands
{
    public class CreateUnitOfMeasurementCommand : IRequest<APIResponseDto>
    {
        public UnitOfMeasurementCreateDto Unit { get; }

        public CreateUnitOfMeasurementCommand(UnitOfMeasurementCreateDto unit)
        {
            Unit = unit;
        }
    }
}
