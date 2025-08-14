using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Commands
{
    public class SoftDeleteCategoryCommandValidator : AbstractValidator<SoftDeleteCategoryCommand>
    {
        public SoftDeleteCategoryCommandValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync(vs.CategoryExistsByIdAsync)
                .WithMessage("Category does not exist.");
        }
    }
}
