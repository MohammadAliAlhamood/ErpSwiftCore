using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductWithTaxesQueryValidator : AbstractValidator<GetProductWithTaxesQuery>
    {
        public GetProductWithTaxesQueryValidator()
        {
            RuleFor(q => q.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    } 
}
