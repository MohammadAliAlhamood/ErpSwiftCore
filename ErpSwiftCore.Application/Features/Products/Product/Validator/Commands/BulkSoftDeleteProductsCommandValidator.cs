using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{
    public class BulkSoftDeleteProductsCommandValidator : AbstractValidator<BulkSoftDeleteProductsCommand>
    {
        public BulkSoftDeleteProductsCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Bulk soft-delete payload is required.")
                .SetValidator(new ProductBulkSoftDeleteDtoValidator());
        }
    }

}
