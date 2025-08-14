using FluentValidation;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductsCountByTypeQueryValidator 
        : AbstractValidator<GetProductsCountByTypeQuery>
    {
        public GetProductsCountByTypeQueryValidator()
        {
            RuleFor(q => q.ProductTypeId)
                .NotEmpty().WithMessage("ProductTypeId is required.");
        }
    } 
}
