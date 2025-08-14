using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Validator.Queries
{
    public class GetUnitOfMeasurementByIdQueryValidator : AbstractValidator<GetUnitOfMeasurementByIdQuery>
    {
        public GetUnitOfMeasurementByIdQueryValidator()
        {
            RuleFor(x => x.UnitId)
                .NotEmpty().WithMessage("Unit ID must be provided.");
        }
    }
}
