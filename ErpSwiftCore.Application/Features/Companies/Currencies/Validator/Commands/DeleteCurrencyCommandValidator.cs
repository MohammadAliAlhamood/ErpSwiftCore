using ErpSwiftCore.Application.Features.Companies.Currencies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Validator.Commands
{
    public class DeleteCurrencyCommandValidator : 
        AbstractValidator<DeleteCurrencyCommand>
    {
        public DeleteCurrencyCommandValidator()
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");
        }
    }
}