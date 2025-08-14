using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{
    public class CompanyAddressDtoValidator : AbstractValidator<AddressDto>
    {
        public CompanyAddressDtoValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().When(x => x.Street != null).WithMessage("الشارع لا يمكن أن يكون فارغًا.");

            RuleFor(x => x.City)
                .NotEmpty().When(x => x.City != null).WithMessage("المدينة لا يمكن أن تكون فارغة.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().When(x => x.PostalCode != null).WithMessage("الرمز البريدي لا يمكن أن يكون فارغًا.");

            RuleFor(x => x.Country)
                .NotEmpty().When(x => x.Country != null).WithMessage("البلد لا يمكن أن يكون فارغًا.");
        }
    }


}
