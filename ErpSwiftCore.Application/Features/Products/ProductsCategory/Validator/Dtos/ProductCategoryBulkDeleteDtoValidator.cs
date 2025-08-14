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
    public class ProductCategoryBulkDeleteDtoValidator : AbstractValidator<ProductCategoryBulkDeleteDto>
    {
        public ProductCategoryBulkDeleteDtoValidator(IProductCategoryValidationService validationService)
        {
            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("CategoryIds must not be empty.");

            RuleForEach(x => x.CategoryIds)
                .MustAsync((id, ct) => validationService.CategoryExistsByIdAsync(id, ct))
                    .WithMessage("One or more categories do not exist.");
        }
    }
}
