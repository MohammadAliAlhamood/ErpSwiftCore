using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{

    public class SearchCompaniesByKeywordQueryValidator : AbstractValidator<SearchCompaniesByKeywordQuery>
    {
        public SearchCompaniesByKeywordQueryValidator()
        {
            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("الكلمة المفتاحية للبحث مطلوبة.");
        }
    }


}
