using ErpSwiftCore.Application.Features.Companies.Currencies.Commands;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Validator.Commands
{
    public class UpdateCurrencyCommandValidator :
        AbstractValidator<UpdateCurrencyCommand>
    {
        public UpdateCurrencyCommandValidator(IValidator<CurrencyUpdateDto> dtoValidator)
        {
            RuleFor(x => x.Currency)
                .NotNull().WithMessage("محتوى تعديل العملة لا يمكن أن يكون فارغًا.")
                .SetValidator(dtoValidator);
        }
    }
}