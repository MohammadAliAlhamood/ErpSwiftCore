using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductsByPriceTypeQueryValidator 
               : AbstractValidator<GetProductsByPriceTypeQuery>
    {
        public GetProductsByPriceTypeQueryValidator()
        {
            RuleFor(q => q.PriceType)
                .NotEmpty().WithMessage("PriceType is required.");
        }
    }


}
