using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductsCountByCategoryQueryValidator :
        AbstractValidator<GetProductsCountByCategoryQuery>
    {
        public GetProductsCountByCategoryQueryValidator()
        {
            RuleFor(q => q.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");
        }
    }
}
