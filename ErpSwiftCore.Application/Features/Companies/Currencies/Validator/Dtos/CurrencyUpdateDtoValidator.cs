using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Validator.Dtos
{
    public class CurrencyUpdateDtoValidator : AbstractValidator<CurrencyUpdateDto>
    {
        private readonly ICurrencyValidationService _validationService;

        public CurrencyUpdateDtoValidator(ICurrencyValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("معرّف العملة مطلوب.");

            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage("يجب إدخال كود العملة.")
                .MustAsync(UniqueCode).WithMessage("كود العملة مستخدم بالفعل.");

            RuleFor(x => x.CurrencyName)
                .NotEmpty().WithMessage("يجب إدخال اسم العملة.");
        }

        private async Task<bool> UniqueCode(CurrencyUpdateDto dto, string code, CancellationToken ct)
        {
            return await _validationService.IsCurrencyCodeUniqueAsync(code, dto.ID, ct);
        }
    }
}