using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{ 
    public class GetProductTaxesByProductQueryValidator : AbstractValidator<GetProductTaxesByProductQuery>
    {
        public GetProductTaxesByProductQueryValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(svc.IsValidProductAsync).WithMessage("Product does not exist.");
        }
    }
}
