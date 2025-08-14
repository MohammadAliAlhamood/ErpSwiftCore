using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Validator.Commands
{
    public class DeleteUnitOfMeasurementCommandValidator : 
        AbstractValidator<DeleteUnitOfMeasurementCommand>
    {
        public DeleteUnitOfMeasurementCommandValidator()
        {
            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Unit ID must be provided.");
        }
    }
}
