using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Commands
{

    public class BulkImportProductBundlesCommandValidator : AbstractValidator<BulkImportProductBundlesCommand>
    {
        public BulkImportProductBundlesCommandValidator(IValidator<ProductBundleCreateDto> dtoValidator)
        {
            RuleFor(x => x.Bundles)
                .NotNull().WithMessage("Bundles list is required.")
                .Must(b => b.Any()).WithMessage("At least one bundle must be provided.")
                .ForEach(bRule => bRule
                    .SetValidator(dtoValidator));
        }
    }

}
