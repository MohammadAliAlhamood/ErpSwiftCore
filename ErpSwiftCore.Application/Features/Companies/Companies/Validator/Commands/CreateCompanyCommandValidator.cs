using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Commands
{

    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator(IValidator<CompanyCreateDto> companyCreateValidator)
        {
            RuleFor(x => x.Company)
                .NotNull()
                .WithMessage("محتوى إنشاء الشركة لا يمكن أن يكون فارغًا.")
                .SetValidator(companyCreateValidator);
        }
    }

}
