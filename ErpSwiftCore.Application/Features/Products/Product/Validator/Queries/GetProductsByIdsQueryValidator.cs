using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{ 
    public class GetProductsByIdsQueryValidator : AbstractValidator<GetProductsByIdsQuery>
    {
        public GetProductsByIdsQueryValidator()
        {
            RuleFor(q => q.ProductIds)
                .NotEmpty().WithMessage("At least one ProductId is required.");
        }
    } 
}
