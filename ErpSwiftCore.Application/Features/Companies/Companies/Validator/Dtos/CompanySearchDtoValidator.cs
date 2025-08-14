using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{

    public class CompanySearchDtoValidator : AbstractValidator<CompanySearchDto>
    {
        public CompanySearchDtoValidator()
        {
            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }
}
