using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductSoftDeleteRangeDtoValidator : AbstractValidator<ProductSoftDeleteRangeDto>
    {
        public ProductSoftDeleteRangeDtoValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotEmpty().WithMessage("At least one ProductId is required.");
        }
    }

}
