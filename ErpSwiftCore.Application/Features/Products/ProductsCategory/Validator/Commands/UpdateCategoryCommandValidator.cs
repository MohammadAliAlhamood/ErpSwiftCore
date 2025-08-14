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
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.Category)
                .NotNull().WithMessage("Category payload is required.")
                .SetValidator(new ProductCategoryUpdateDtoValidator(vs));
        }
    }



}
