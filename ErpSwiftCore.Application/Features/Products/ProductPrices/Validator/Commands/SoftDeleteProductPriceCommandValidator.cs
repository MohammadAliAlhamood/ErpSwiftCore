using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{
    public class SoftDeleteProductPriceCommandValidator : AbstractValidator<SoftDeleteProductPriceCommand>
    {
        public SoftDeleteProductPriceCommandValidator(IProductPriceValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.PriceId)
                .NotEmpty().WithMessage("PriceId is required.")
                .MustAsync(vs.PriceExistsByIdAsync).WithMessage("Price record not found.");
        }
    }



}
