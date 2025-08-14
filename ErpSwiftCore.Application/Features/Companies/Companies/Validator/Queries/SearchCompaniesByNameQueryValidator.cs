using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{
    public class SearchCompaniesByNameQueryValidator : AbstractValidator<SearchCompaniesByNameQuery>
    {
        public SearchCompaniesByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الشركة للبحث مطلوب.");
        }
    }

}
