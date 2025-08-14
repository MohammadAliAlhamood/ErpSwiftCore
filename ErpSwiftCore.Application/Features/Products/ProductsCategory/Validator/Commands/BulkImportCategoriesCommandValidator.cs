using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Commands
{

    public class BulkImportCategoriesCommandValidator
        : AbstractValidator<BulkImportCategoriesCommand>
    {
        public BulkImportCategoriesCommandValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.Categories)
                .NotEmpty().WithMessage("At least one category is required.")
                .ForEach(cat => cat.SetValidator(new ProductCategoryCreateDtoValidator(vs)));
        }
    }

}
