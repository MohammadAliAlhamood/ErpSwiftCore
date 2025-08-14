using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{
    public class DeleteProductCommandValidator 
        
        : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator(IProductValidationService vs)
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Delete payload is required.")
                .SetValidator(new ProductDeleteDtoValidator(vs));
        }
    }

}
