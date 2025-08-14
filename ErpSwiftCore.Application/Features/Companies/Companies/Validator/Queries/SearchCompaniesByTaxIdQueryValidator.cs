using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Validator.Queries
{
    public class SearchCompaniesByTaxIdQueryValidator : AbstractValidator<SearchCompaniesByTaxIdQuery>
    {
        public SearchCompaniesByTaxIdQueryValidator()
        {
            RuleFor(x => x.TaxId)
                .NotEmpty().WithMessage("الرقم الضريبي للبحث مطلوب.");
        }
    }

}
