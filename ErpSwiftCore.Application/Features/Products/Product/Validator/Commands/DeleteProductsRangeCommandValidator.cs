using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Commands
{

    public class DeleteProductsRangeCommandValidator : AbstractValidator<DeleteProductsRangeCommand>
    {
        public DeleteProductsRangeCommandValidator()
        {
            RuleFor(cmd => cmd.Dto)
                .NotNull().WithMessage("Delete range payload is required.")
                .SetValidator(new ProductDeleteRangeDtoValidator());
        }
    }


}
