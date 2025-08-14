using FluentValidation;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Dtos
{
    public class ProductPriceCreateDtoValidator : AbstractValidator<ProductPriceCreateDto>
    {
        public ProductPriceCreateDtoValidator(IProductPriceValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(vs.IsValidProductAsync)
                .WithMessage("Product does not exist or is deleted.");

            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage("CurrencyId is required.")
                .MustAsync(vs.IsValidCurrencyAsync)
                .WithMessage("Currency does not exist or is deleted.");

            RuleFor(x => x.Price)
                .GreaterThan(0M).WithMessage("Price must be positive.")
                .MustAsync(vs.IsPricePositiveAsync)
                .WithMessage("Price must be positive.");
 
            RuleFor(x => x.EffectiveDate)
                .NotEmpty().WithMessage("EffectiveDate is required.")
                .MustAsync(vs.IsEffectiveDateValidAsync)
                .WithMessage("Effective date cannot be in the future.");
        }
    }

  
   

  
}
