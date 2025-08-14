using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Queries
{
    public class GetCategoriesCountByParentQueryValidator : AbstractValidator<GetCategoriesCountByParentQuery>
    {
        public GetCategoriesCountByParentQueryValidator()
        {
            RuleFor(x => x.ParentCategoryId).NotEmpty().WithMessage("ParentCategoryId is required.");
        }
    }



}
