using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductDeleteRangeDtoValidator : 
        AbstractValidator<ProductDeleteRangeDto>
    {
        public ProductDeleteRangeDtoValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotEmpty().WithMessage("At least one ProductId is required.");
        }
    } 
}
