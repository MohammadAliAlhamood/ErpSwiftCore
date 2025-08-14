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
    public class ProductCategoryBulkImportDtoValidator : AbstractValidator<ProductCategoryBulkImportDto>
    {
        public ProductCategoryBulkImportDtoValidator(IProductCategoryValidationService validationService)
        {
            RuleFor(x => x.Categories)
                .NotEmpty().WithMessage("Categories must not be empty.");

            RuleForEach(x => x.Categories)
                .SetValidator(new ProductCategoryCreateDtoValidator(validationService));
        }
    }
}
