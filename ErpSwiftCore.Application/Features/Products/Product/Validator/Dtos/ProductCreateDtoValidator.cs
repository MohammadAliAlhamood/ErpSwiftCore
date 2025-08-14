using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos; 
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Products.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator(IProductValidationService vs)
        {
            ClassLevelCascadeMode = CascadeMode.Stop; 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100)
                .MustAsync((name, ct) => vs.IsNameValidAsync(name, ct)).WithMessage("Invalid name.")
                .MustAsync((name, ct) => vs.IsProductNameUniqueAsync(name, null, ct)).WithMessage("Name must be unique.");
            RuleFor(x => x.Code)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.Code))
                .MustAsync((code, ct) => vs.IsProductCodeUniqueAsync(code, null, ct)).WithMessage("Code must be unique.");
            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .MustAsync((sku, ct) => vs.IsSkuValidAsync(sku, ct)).WithMessage("Invalid SKU.")
                .MustAsync((sku, ct) => vs.IsSkuUniqueAsync(sku, null, ct)).WithMessage("SKU must be unique.");
            RuleFor(x => x.Barcode)
                .NotEmpty().WithMessage("Barcode is required.")
                .MustAsync((barcode, ct) => vs.IsBarcodeValidAsync(barcode, ct)).WithMessage("Invalid barcode.")
                .MustAsync((barcode, ct) => vs.IsBarcodeUniqueAsync(barcode, null, ct)).WithMessage("Barcode must be unique.");
        }
    } 
}
