using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{
    #region ──────────── Company DTO Validators ────────────

    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        private readonly ICompanyValidationService _validationService;

        public CompanyCreateDtoValidator(ICompanyValidationService validationService)
        {
            _validationService = validationService;

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

        private async Task<bool> UniqueName(string companyName, CancellationToken ct)
        {
            return await _validationService.IsCompanyNameUniqueAsync(companyName, null, ct);
        }

 

        private async Task<bool> UniqueEmail(string email, CancellationToken ct)
        {
            return await _validationService.IsCompanyEmailUniqueAsync(email, null, ct);
        }
    }

 
    #endregion

 
 
}
