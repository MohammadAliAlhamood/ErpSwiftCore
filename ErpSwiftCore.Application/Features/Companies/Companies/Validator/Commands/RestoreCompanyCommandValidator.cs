using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Commands
{

    public class RestoreCompanyCommandValidator : AbstractValidator<RestoreCompanyCommand>
    {
        public RestoreCompanyCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

}
