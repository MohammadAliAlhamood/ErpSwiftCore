using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Dtos
{
    public class CompanyPagedResultDtoValidator : AbstractValidator<CompanyPagedResultDto>
    {
        public CompanyPagedResultDtoValidator()
        {
            RuleFor(x => x.Companies)
                .NotNull().WithMessage("قائمة الشركات لا يمكن أن تكون فارغة.");

            RuleFor(x => x.TotalCount)
                .GreaterThanOrEqualTo(0).WithMessage("عدد الشركات الكلي يجب أن يكون صفراً أو أكثر.");
        }
    }

}
