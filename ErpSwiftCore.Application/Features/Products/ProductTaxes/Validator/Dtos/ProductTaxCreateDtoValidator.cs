using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos
{
    public class ProductTaxCreateDtoValidator : AbstractValidator<ProductTaxCreateDto>
    {
        public ProductTaxCreateDtoValidator(IProductTaxValidationService validatorService)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync((id, ct) => validatorService.IsValidProductAsync(id, ct))
                .WithMessage("Product does not exist.");

            RuleFor(x => x.Rate)
                .MustAsync((rate, ct) => validatorService.IsRateValidAsync(rate, ct))
                .WithMessage("Rate must be between allowed bounds.");
        }
    }

  
   
 
   
}







