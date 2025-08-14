using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{

    public class GetCompaniesPagedByIndustryQueryValidator : AbstractValidator<GetCompaniesPagedByIndustryQuery>
    {
        public GetCompaniesPagedByIndustryQueryValidator()
        {
            RuleFor(x => x.Industry)
                .NotEmpty().WithMessage("نوع الصناعة مطلوب.");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("صفحة البداية يجب أن تكون 0 أو أكثر.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("حجم الصفحة يجب أن يكون أكبر من صفر.");
        }
    }


}
