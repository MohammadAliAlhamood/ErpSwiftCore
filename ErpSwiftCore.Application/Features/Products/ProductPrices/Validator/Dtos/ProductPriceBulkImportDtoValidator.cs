using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Dtos
{
    public class ProductPriceBulkImportDtoValidator : AbstractValidator<ProductPriceBulkImportDto>
    {
        public ProductPriceBulkImportDtoValidator(IValidator<ProductPriceCreateDto> createValidator)
        {
            RuleFor(x => x.Prices)
                .NotEmpty().WithMessage("At least one price must be imported.");

            RuleForEach(x => x.Prices)
                .SetValidator(createValidator);
        }
    }


}
