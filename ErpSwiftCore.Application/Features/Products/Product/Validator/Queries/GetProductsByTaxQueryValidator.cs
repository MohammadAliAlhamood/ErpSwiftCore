using ErpSwiftCore.Application.Features.Products.Product.Queries;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductsByTaxQueryValidator : 
                 AbstractValidator<GetProductsByTaxQuery>
    {
        public GetProductsByTaxQueryValidator()
        {
            RuleFor(q => q.TaxId)
                .NotEmpty().WithMessage("TaxId is required.");
        }
    }


}
