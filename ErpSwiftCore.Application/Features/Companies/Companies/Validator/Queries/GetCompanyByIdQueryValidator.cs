using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{



    public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
    {
        public GetCompanyByIdQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }
}
