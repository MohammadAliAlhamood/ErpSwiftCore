using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductBulkImportDtoValidator : 
                 AbstractValidator<ProductBulkImportDto>
    {
        public ProductBulkImportDtoValidator()
        {
            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("Products collection is required.");
        }
    } 
}
