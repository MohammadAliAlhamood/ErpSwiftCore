using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Products.Validators;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Dtos
{ 
    public class ProductUpdateDtoValidator 
        : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator(IProductValidationService vs)
        {
            Include(new ProductCreateDtoValidator(vs)); 
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("ID is required.")
                .MustAsync((id, ct) => vs.ProductExistsByIdAsync(id, ct)).WithMessage("Product not found.");
        }
    } 
}
