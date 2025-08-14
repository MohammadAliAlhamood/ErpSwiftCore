using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Dtos
{
    public class ProductCategoryDeleteDtoValidator : AbstractValidator<ProductCategoryDeleteDto>
    {
        public ProductCategoryDeleteDtoValidator(IProductCategoryValidationService validationService)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync((id, ct) => validationService.CategoryExistsByIdAsync(id, ct))
                    .WithMessage("Category does not exist.");
        }
    }

}
