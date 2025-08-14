using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductWithBundlesQueryValidator : 
                 AbstractValidator<GetProductWithBundlesQuery>
    {
        public GetProductWithBundlesQueryValidator()
        {
            RuleFor(q => q.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    }

}
