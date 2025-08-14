using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{

    public class BulkUpdateStockCommandValidator 
        : AbstractValidator<BulkUpdateStockCommand>
    {
        public BulkUpdateStockCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Bulk stock-update payload is required.")
                .SetValidator(new ProductBulkUpdateStockDtoValidator());
        }
    }

}
