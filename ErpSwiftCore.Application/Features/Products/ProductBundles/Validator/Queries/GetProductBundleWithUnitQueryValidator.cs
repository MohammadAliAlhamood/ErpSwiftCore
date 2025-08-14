using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundleWithUnitQueryValidator 
        : AbstractValidator<GetProductBundleWithUnitQuery>
    {
        public GetProductBundleWithUnitQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.BundleId)
                .NotEmpty().WithMessage("BundleId is required.")
                .MustAsync(async (id, ct) => await validationService.BundleExistsByIdAsync(id, ct))
                .WithMessage("Specified bundle does not exist.");
        }
    }


}
