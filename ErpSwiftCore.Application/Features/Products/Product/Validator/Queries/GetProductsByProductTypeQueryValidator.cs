using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductsByProductTypeQueryValidator : AbstractValidator<GetProductsByProductTypeQuery>
    {
        public GetProductsByProductTypeQueryValidator()
        {
            RuleFor(q => q.ProductTypeId)
                .NotEmpty().WithMessage("ProductTypeId is required.");
        }
    } 
}
