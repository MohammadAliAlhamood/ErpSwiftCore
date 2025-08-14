using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{

    public class CompanyUpdateDtoValidator : AbstractValidator<CompanyUpdateDto>
    {
        private readonly ICompanyValidationService _validationService;

        public CompanyUpdateDtoValidator(ICompanyValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.")
                .MustAsync(ExistsById).WithMessage("الشركة غير موجودة.");

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("يجب إدخال اسم الشركة.")
                .MustAsync(UniqueName).WithMessage("اسم الشركة مستخدم بالفعل.");

            RuleFor(x => x.TaxID)
                .NotEmpty().WithMessage("يجب إدخال رقم التعريف الضريبي.");

            RuleFor(x => x.IndustryType)
                .NotEmpty().WithMessage("يجب إدخال نوع الصناعة.");

            RuleFor(x => x.ContactInfo)
                .NotNull().WithMessage("معلومات الاتصال مطلوبة.");

            When(x => x.ContactInfo != null, () =>
            {
                RuleFor(x => x.ContactInfo.Email)
                    .NotEmpty().WithMessage("يجب إدخال البريد الإلكتروني.")
                    .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.")
                    .MustAsync(UniqueEmail).WithMessage("البريد الإلكتروني مستخدم بالفعل.");
            });
        }

        private async Task<bool> ExistsById(Guid id, CancellationToken ct)
        {
            return await _validationService.CompanyExistsByIdAsync(id, ct);
        }

        private async Task<bool> UniqueName(CompanyUpdateDto dto, string companyName, CancellationToken ct)
        {
            return await _validationService.IsCompanyNameUniqueAsync(companyName, dto.Id, ct);
        }

        private async Task<bool> UniqueEmail(CompanyUpdateDto dto, string email, CancellationToken ct)
        {
            return await _validationService.IsCompanyEmailUniqueAsync(email, dto.Id, ct);
        }
    }

}
