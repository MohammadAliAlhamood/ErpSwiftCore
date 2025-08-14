using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Validator.Commands
{
    public class DeleteUnitsOfMeasurementRangeCommandValidator : AbstractValidator<DeleteUnitsOfMeasurementRangeCommand>
    {
        public DeleteUnitsOfMeasurementRangeCommandValidator()
        {
            RuleFor(x => x.UnitIds)
                .NotNull().WithMessage("Unit IDs must be provided.")
                .Must(x => x.Any()).WithMessage("At least one Unit ID must be provided.");
        }
    }
}
