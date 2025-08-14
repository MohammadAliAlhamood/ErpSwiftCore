using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductsByCategoryQueryValidator : 
                 AbstractValidator<GetProductsByCategoryQuery>
    {
        public GetProductsByCategoryQueryValidator()
        {
            RuleFor(q => q.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");
        }
    }


}
