using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Commands
{
    public class DeleteProductBundlesRangeCommandValidator : AbstractValidator<DeleteProductBundlesRangeCommand>
    {
        public DeleteProductBundlesRangeCommandValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.BundleIds)
                .NotNull().WithMessage("BundleIds list is required.")
                .Must(ids => ids.Any()).WithMessage("At least one BundleId must be provided.")
                .ForEach(idRule => idRule
                    .NotEmpty().WithMessage("BundleId cannot be empty.")
                    .MustAsync(async (bundleId, ct) => await validationService.BundleExistsByIdAsync(bundleId, ct))
                    .WithMessage("One or more specified bundles do not exist."));
        }
    }

}
