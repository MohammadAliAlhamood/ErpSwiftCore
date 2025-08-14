using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Validator.Commands
{
    public class BulkImportProductPricesCommandValidator : AbstractValidator<BulkImportProductPricesCommand>
    {
        public BulkImportProductPricesCommandValidator(IValidator<ProductPriceCreateDto> dtoValidator)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Prices)
                .NotEmpty().WithMessage("At least one price must be provided.")
                .ForEach(r => r.SetValidator(dtoValidator));
        }
    }


}
