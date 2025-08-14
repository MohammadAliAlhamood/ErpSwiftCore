using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Queries
{

    public class GetSoftDeletedCategoryByIdQueryValidator : AbstractValidator<GetSoftDeletedCategoryByIdQuery>
    {
        public GetSoftDeletedCategoryByIdQueryValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync(vs.CategoryExistsByIdAsync).WithMessage("Soft‑deleted category does not exist.");
        }
    }

}
