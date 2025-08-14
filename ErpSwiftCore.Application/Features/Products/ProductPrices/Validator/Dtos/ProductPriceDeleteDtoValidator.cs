using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Dtos
{
    public class ProductPriceDeleteDtoValidator : AbstractValidator<ProductPriceDeleteDto>
    {
        public ProductPriceDeleteDtoValidator(IProductPriceValidationService vs)
        {
            RuleFor(x => x.PriceId)
                .NotEmpty().WithMessage("PriceId is required.")
                .MustAsync(vs.PriceExistsByIdAsync)
                .WithMessage("Price record not found.");
        }
    }


}
