using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{
    public class GetProductByBarcodeQueryValidator : 
        AbstractValidator<GetProductByBarcodeQuery>
    {
        public GetProductByBarcodeQueryValidator()
        {
            RuleFor(q => q.Barcode)
                .NotEmpty().WithMessage("Barcode is required.");
        }
    }
}
