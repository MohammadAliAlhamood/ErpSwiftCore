using ErpSwiftCore.Application.Features.Products.Product.Queries;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductBySkuQueryValidator : AbstractValidator<GetProductBySkuQuery>
    {
        public GetProductBySkuQueryValidator()
        {
            RuleFor(q => q.SKU)
                .NotEmpty().WithMessage("SKU is required.");
        }
    } 
}
