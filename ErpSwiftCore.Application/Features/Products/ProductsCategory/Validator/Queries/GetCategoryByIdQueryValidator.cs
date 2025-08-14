using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
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
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator(IProductCategoryValidationService vs)
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync(vs.CategoryExistsByIdAsync).WithMessage("Category does not exist.");
        }
    }




    public class BasePagingValidator<T> : AbstractValidator<T> where T : BasePageParamDto
    {
        public BasePagingValidator()
        {
            RuleFor(x => x.pageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("pageIndex must be ≥ 0.");
            RuleFor(x => x.pageSize)
                .GreaterThan(0).WithMessage("pageSize must be > 0.");
        }
    } 
    public class SimpleIdQueryValidator<T> : AbstractValidator<T> where T : class
    {
        public SimpleIdQueryValidator(Func<T, Guid> selector)
        {
            RuleFor(x => selector(x))
                .NotEmpty().WithMessage("ID is required.");
        }
    }


   

  

    
   




}