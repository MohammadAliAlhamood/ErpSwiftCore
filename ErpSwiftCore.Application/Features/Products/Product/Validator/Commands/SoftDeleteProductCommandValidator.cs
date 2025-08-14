using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{

    public class SoftDeleteProductCommandValidator : AbstractValidator<SoftDeleteProductCommand>
    {
        public SoftDeleteProductCommandValidator(IProductValidationService vs)
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Soft‑delete payload is required.")
                .SetValidator(new ProductSoftDeleteDtoValidator(vs));
        }
    }


}
