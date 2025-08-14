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
    public class ProductCategoryCreateDtoValidator : AbstractValidator<ProductCategoryCreateDto>
    {
        public ProductCategoryCreateDtoValidator(IProductCategoryValidationService validationService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.")
                .MustAsync((dto, name, ct) => validationService.IsNameValidAsync(name, ct))
                    .WithMessage("Name contains invalid characters.")
                .MustAsync((dto, name, ct) => validationService.IsCategoryNameUniqueAsync(name, dto.ParentCategoryId, null, ct))
                    .WithMessage("A category with the same name already exists under the specified parent.");

            When(x => x.ParentCategoryId.HasValue, () =>
            {
                RuleFor(x => x.ParentCategoryId.Value)
                    .MustAsync((parentId, ct) => validationService.IsValidParentCategoryAsync(parentId, null, ct))
                        .WithMessage("Parent category is invalid or does not exist.");
            });
        }
    } 
}