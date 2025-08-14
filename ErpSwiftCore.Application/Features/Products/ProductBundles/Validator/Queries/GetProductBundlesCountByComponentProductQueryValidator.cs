using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundlesCountByComponentProductQueryValidator
        : AbstractValidator<GetProductBundlesCountByComponentProductQuery>
    {
        public GetProductBundlesCountByComponentProductQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.ComponentProductId)
                .NotEmpty().WithMessage("ComponentProductId is required.")
                .MustAsync(async (id, ct) => await validationService.IsValidComponentProductAsync(id, ct))
                .WithMessage("Specified component product does not exist or is invalid.");
        }
    }


}
