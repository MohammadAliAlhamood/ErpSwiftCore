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
    public class ProductCategoryUpdateDtoValidator : AbstractValidator<ProductCategoryUpdateDto>
    {
        public ProductCategoryUpdateDtoValidator(IProductCategoryValidationService validationService)
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("ID is required.")
                .MustAsync((id, ct) => validationService.CategoryExistsByIdAsync(id, ct))
                    .WithMessage("Category does not exist.");

            Include(new ProductCategoryCreateDtoValidator(validationService));

            When(x => x.ParentCategoryId.HasValue, () =>
            {
                RuleFor(x => x.ParentCategoryId.Value)
                    .MustAsync((dto, parentId, ct) =>
                        validationService.IsValidParentCategoryAsync(parentId, dto.ID, ct))
                    .WithMessage("Parent category is invalid or does not exist.");

                RuleFor(x => x)
                    .MustAsync((dto, ct) =>
                        validationService.ExistsCircularDependencyAsync(dto.ID, dto.ParentCategoryId, ct)
                            .ContinueWith(task => !task.Result, ct))
                    .WithMessage("Circular dependency detected with parent category.");
            });

            RuleFor(x => x.Name)
                .MustAsync((dto, name, ct) =>
                    validationService.IsCategoryNameUniqueAsync(name, dto.ParentCategoryId, dto.ID, ct))
                .WithMessage("A category with the same name already exists under the specified parent.");
        }
    }

}
