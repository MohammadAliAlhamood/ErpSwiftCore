using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Dtos
{
    public class ProductPriceUpdateDtoValidator : AbstractValidator<ProductPriceUpdateDto>
    {
        public ProductPriceUpdateDtoValidator(IProductPriceValidationService vs)
        {
            Include(new ProductPriceCreateDtoValidator(vs));
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(vs.PriceExistsByIdAsync)
                .WithMessage("Price record not found.");
        }
    }
}
