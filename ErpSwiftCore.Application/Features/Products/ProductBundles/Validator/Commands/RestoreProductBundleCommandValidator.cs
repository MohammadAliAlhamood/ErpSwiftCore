using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Commands
{
    public class RestoreProductBundleCommandValidator : AbstractValidator<RestoreProductBundleCommand>
    {
        public RestoreProductBundleCommandValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.BundleId)
                .NotEmpty().WithMessage("BundleId is required.")
                .MustAsync(async (id, ct) => await validationService.BundleExistsByIdAsync(id, ct))
                .WithMessage("Specified bundle does not exist.");
        }
    }


}
