using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{
    public class BulkRestoreProductsCommandValidator 
        : AbstractValidator<BulkRestoreProductsCommand>
    {
        public BulkRestoreProductsCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Bulk restore payload is required.")
                .SetValidator(new ProductBulkRestoreDtoValidator());
        }
    }
}
