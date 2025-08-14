using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Commands
{
    public class CreateProductBundleCommandValidator : AbstractValidator<CreateProductBundleCommand>
    {
        public CreateProductBundleCommandValidator(IValidator<ProductBundleCreateDto> dtoValidator)
        {
            RuleFor(x => x.Bundle)
                .NotNull().WithMessage("Bundle payload is required.")
                .SetValidator(dtoValidator);
        }
    } 
}