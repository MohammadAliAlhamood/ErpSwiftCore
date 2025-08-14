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

    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator(IValidator<CompanyUpdateDto> companyUpdateValidator)
        {
            RuleFor(x => x.Company)
                .NotNull()
                .WithMessage("محتوى تحديث الشركة لا يمكن أن يكون فارغًا.")
                .SetValidator(companyUpdateValidator);
        }
    }

}
