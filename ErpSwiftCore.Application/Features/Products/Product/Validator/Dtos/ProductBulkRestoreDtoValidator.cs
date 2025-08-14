using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductBulkRestoreDtoValidator : AbstractValidator<ProductBulkRestoreDto>
    {
        public ProductBulkRestoreDtoValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotEmpty().WithMessage("At least one ProductId is required.");
        }
    }

}
