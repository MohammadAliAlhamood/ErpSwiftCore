using System;
using System.Collections.Generic;
using FluentValidation;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Commands
{
    // Abstract base for commands accepting a list of IDs
    public abstract class IdListCommandValidator<T> : AbstractValidator<T> where T : class
    {
        protected IdListCommandValidator(Func<T, IEnumerable<Guid>> selector, IProductCategoryValidationService vs)
        {
            RuleFor(x => selector(x))
                .NotEmpty().WithMessage("IDs collection must not be empty.")
                .ForEach(id => id
                    .MustAsync((id, ct) => vs.CategoryExistsByIdAsync(id, ct))
                    .WithMessage("One or more categories do not exist."));
        }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.Category)
                .NotNull().WithMessage("Category payload is required.")
                .SetValidator(new ProductCategoryCreateDtoValidator(vs));
        }
    }

  
   


}
