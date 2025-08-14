using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductBulkUpdateStockDtoValidator : AbstractValidator<ProductBulkUpdateStockDto>
    {
        public ProductBulkUpdateStockDtoValidator()
        {
            RuleFor(x => x.StockUpdates)
                .NotEmpty().WithMessage("StockUpdates collection is required.");

            RuleForEach(x => x.StockUpdates).ChildRules(upd =>
            {
                upd.RuleFor(u => u.ProductId)
                   .NotEmpty().WithMessage("ProductId is required.");
                upd.RuleFor(u => u.Quantity)
                   .GreaterThanOrEqualTo(0).WithMessage("Quantity must be >= 0.");
            });
        }
    } 
}
