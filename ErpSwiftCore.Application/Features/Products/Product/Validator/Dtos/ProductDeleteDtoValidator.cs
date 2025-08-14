using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{
    public class ProductDeleteDtoValidator 
        : AbstractValidator<ProductDeleteDto>
    {
        public ProductDeleteDtoValidator(IProductValidationService vs)
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync((id, ct) => vs.ProductExistsByIdAsync(id, ct)).WithMessage("Product not found.");
        }
    }

}
