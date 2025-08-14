using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{

    public class CompanyCountDtoValidator : AbstractValidator<CompanyCountDto>
    {
        public CompanyCountDtoValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("العدد يجب أن يكون صفرًا أو أكثر.");
        }
    }
}
