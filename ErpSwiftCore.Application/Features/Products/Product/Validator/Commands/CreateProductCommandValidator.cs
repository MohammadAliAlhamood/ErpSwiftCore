using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Products.Validators;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IProductValidationService vs)
        {
            RuleFor(cmd => cmd.Product)
                .NotNull()
                .WithMessage("Product payload is required.")
                .SetValidator(new ProductCreateDtoValidator(vs));
        }
    } 
}
