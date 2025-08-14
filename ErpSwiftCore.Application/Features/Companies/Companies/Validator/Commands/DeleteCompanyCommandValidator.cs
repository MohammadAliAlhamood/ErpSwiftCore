using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Commands
{

    public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        public DeleteCompanyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرّف الشركة مطلوب.");
        }
    }

}
