using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Validator.Commands
{
    public class CreateUnitOfMeasurementCommandValidator : AbstractValidator<CreateUnitOfMeasurementCommand>
    {
        public CreateUnitOfMeasurementCommandValidator()
        {
            RuleFor(x => x.Unit)
                .NotNull().WithMessage("Unit data must be provided.");

            RuleFor(x => x.Unit.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Unit.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MaximumLength(20).WithMessage("Abbreviation must not exceed 20 characters.");

            RuleFor(x => x.Unit.Description)
                .MaximumLength(250).WithMessage("Description must not exceed 250 characters.")
                .When(x => !string.IsNullOrEmpty(x.Unit.Description));
        }
    }

}
