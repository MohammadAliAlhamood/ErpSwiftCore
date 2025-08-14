using ErpSwiftCore.Application.Features.Companies.Currencies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Validator.Queries
{
    public class GetCurrencyByIdQueryValidator : AbstractValidator<GetCurrencyByIdQuery>
    {
        public GetCurrencyByIdQueryValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");
        }
    }
}