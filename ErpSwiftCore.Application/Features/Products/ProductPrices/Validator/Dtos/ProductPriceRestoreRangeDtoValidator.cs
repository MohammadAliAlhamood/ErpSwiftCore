using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Dtos
{
    public class ProductPriceRestoreRangeDtoValidator : AbstractValidator<ProductPriceRestoreRangeDto>
    {
        public ProductPriceRestoreRangeDtoValidator(IProductPriceValidationService vs)
        {
            RuleFor(x => x.PriceIds)
                .NotEmpty().WithMessage("At least one PriceId must be provided.")
                .MustAsync(async (ids, ct) =>
                    ids != null && ids.All(id => vs.PriceExistsByIdAsync(id, ct).Result))
                .WithMessage("One or more PriceIds do not exist.");
        }
    }


}
