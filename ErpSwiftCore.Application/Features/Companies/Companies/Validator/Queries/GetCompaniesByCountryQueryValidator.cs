using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{

    public class GetCompaniesByCountryQueryValidator : AbstractValidator<GetCompaniesByCountryQuery>
    {
        public GetCompaniesByCountryQueryValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("الدولة مطلوبة.");
        }
    }

}
