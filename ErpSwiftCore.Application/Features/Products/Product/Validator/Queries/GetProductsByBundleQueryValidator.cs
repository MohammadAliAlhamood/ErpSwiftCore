using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductsByBundleQueryValidator : AbstractValidator<GetProductsByBundleQuery>
    {
        public GetProductsByBundleQueryValidator()
        {
            RuleFor(q => q.BundleId)
                .NotEmpty().WithMessage("BundleId is required.");
        }
    }


}
