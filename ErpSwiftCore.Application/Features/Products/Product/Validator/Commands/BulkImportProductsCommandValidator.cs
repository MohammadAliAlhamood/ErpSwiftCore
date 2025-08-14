using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{

    public class BulkImportProductsCommandValidator 
        : AbstractValidator<BulkImportProductsCommand>
    {
        public BulkImportProductsCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Bulk import payload is required.")
                .SetValidator(new ProductBulkImportDtoValidator());
        }
    }

}
