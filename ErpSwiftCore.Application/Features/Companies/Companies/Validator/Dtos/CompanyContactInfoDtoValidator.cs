using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{
    public class CompanyContactInfoDtoValidator : AbstractValidator<ContactInfoDto>
    {
        public CompanyContactInfoDtoValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");
            });

            RuleFor(x => x.Phone)
                .NotEmpty().When(x => x.Phone != null).WithMessage("رقم الهاتف لا يمكن أن يكون فارغًا.");

            RuleFor(x => x.Mobile)
                .NotEmpty().When(x => x.Mobile != null).WithMessage("رقم الجوال لا يمكن أن يكون فارغًا.");
        }
    }

}
