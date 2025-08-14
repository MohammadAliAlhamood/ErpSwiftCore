using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundlesByComponentProductQueryValidator 
        : AbstractValidator<GetProductBundlesByComponentProductQuery>
    {
        public GetProductBundlesByComponentProductQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.ComponentProductId)
                .NotEmpty().WithMessage("ComponentProductId is required.")
                .MustAsync(async (id, ct) => await validationService.IsValidComponentProductAsync(id, ct))
                .WithMessage("Specified component product does not exist or is invalid.");
        }
    }


}
