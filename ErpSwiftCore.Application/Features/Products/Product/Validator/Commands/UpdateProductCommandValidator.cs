using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IProductValidationService vs)
        {
            RuleFor(cmd => cmd.Product)
                .NotNull().WithMessage("Product payload is required.")
                .SetValidator(new ProductUpdateDtoValidator(vs));
        }
    }


}
