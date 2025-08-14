using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{

    public class GetCompanyByNameQueryValidator : AbstractValidator<GetCompanyByNameQuery>
    {
        public GetCompanyByNameQueryValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("اسم الشركة مطلوب.");
        }
    }

}
