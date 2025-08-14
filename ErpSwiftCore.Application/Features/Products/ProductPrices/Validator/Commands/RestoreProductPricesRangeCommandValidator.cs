using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{
    public class RestoreProductPricesRangeCommandValidator : AbstractValidator<RestoreProductPricesRangeCommand>
    {
        public RestoreProductPricesRangeCommandValidator(IProductPriceValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.PriceIds)
                .NotEmpty().WithMessage("At least one PriceId must be provided.")
                .MustAsync(async (ids, ct) =>
                {
                    if (ids == null) return false;
                    foreach (var id in ids)
                    {
                        if (!await vs.PriceExistsByIdAsync(id, ct))
                            return false;
                    }
                    return true;
                })
                .WithMessage("One or more PriceIds do not exist.");
        }
    }

}
