using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundleByIdQueryValidator : AbstractValidator<GetProductBundleByIdQuery>
    {
        public GetProductBundleByIdQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.BundleId)
                .NotEmpty().WithMessage("BundleId is required.")
                .MustAsync(async (id, ct) => await validationService.BundleExistsByIdAsync(id, ct))
                .WithMessage("Specified bundle does not exist.");
        }
    } 
}
