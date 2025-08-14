using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductRestoreRangeDtoValidator : AbstractValidator<ProductRestoreRangeDto>
    {
        public ProductRestoreRangeDtoValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotEmpty().WithMessage("At least one ProductId is required.");
        }
    }

}
