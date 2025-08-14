using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Commands
{
    public class BulkCreateCompaniesCommandValidator : AbstractValidator<BulkCreateCompaniesCommand>
    {
        public BulkCreateCompaniesCommandValidator(IValidator<CompanyCreateDto> companyCreateValidator)
        {
            RuleFor(x => x.Companies)
                .NotNull().WithMessage("قائمة الشركات لا يمكن أن تكون فارغة.")
                .Must(list => list != null && list.Any())
                    .WithMessage("يجب إدخال شركة واحدة على الأقل.");

            RuleForEach(x => x.Companies)
                .SetValidator(companyCreateValidator);
        }
    }
}
