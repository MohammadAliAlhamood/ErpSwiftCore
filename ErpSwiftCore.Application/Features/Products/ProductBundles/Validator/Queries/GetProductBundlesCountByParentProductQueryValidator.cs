using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundlesCountByParentProductQueryValidator
        : AbstractValidator<GetProductBundlesCountByParentProductQuery>
    {
        public GetProductBundlesCountByParentProductQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.ParentProductId)
                .NotEmpty().WithMessage("ParentProductId is required.")
                .MustAsync(async (id, ct) => await validationService.IsValidParentProductAsync(id, ct))
                .WithMessage("Specified parent product does not exist or is invalid.");
        }
    }


}
