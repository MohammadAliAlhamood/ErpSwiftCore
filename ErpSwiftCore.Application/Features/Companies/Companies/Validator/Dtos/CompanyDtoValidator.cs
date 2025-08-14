using FluentValidation; 
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator()
        {
            RuleFor(x => x.Id)  .NotEmpty().WithMessage("المعرّف لا يمكن أن يكون فارغًا."); 
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("يجب إدخال اسم الشركة."); 
 
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
                    .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");
            }); 
        }
    } 
}
