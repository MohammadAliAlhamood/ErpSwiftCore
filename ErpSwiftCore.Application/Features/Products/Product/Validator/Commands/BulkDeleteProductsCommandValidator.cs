using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{ 
    public class BulkDeleteProductsCommandValidator 
        : AbstractValidator<BulkDeleteProductsCommand>
    {
        public BulkDeleteProductsCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Bulk delete payload is required.")
                .SetValidator(new ProductBulkDeleteDtoValidator());
        }
    } 
}
