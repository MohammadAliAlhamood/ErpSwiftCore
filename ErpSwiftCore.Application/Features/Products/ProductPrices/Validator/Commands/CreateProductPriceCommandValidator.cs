using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{
    public class CreateProductPriceCommandValidator : AbstractValidator<CreateProductPriceCommand>
    {
        public CreateProductPriceCommandValidator(IValidator<ProductPriceCreateDto> dtoValidator)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Price).SetValidator(dtoValidator);
        }
    }

   

 


     
  
}