using ErpSwiftCore.Application.Features.Products.Product.Queries;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductWithUnitsQueryValidator : AbstractValidator<GetProductWithUnitsQuery>
    {
        public GetProductWithUnitsQueryValidator()
        {
            RuleFor(q => q.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    }


}
