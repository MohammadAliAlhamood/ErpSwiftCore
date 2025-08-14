using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{

    public class ProductRestoreDtoValidator 
        : AbstractValidator<ProductRestoreDto>
    {
        public ProductRestoreDtoValidator(IProductValidationService vs)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync((id, ct) => vs.ProductExistsByIdAsync(id, ct)).WithMessage("Product not found.");
        }
    }

}
