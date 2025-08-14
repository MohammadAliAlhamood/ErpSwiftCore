using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Queries
{
    public class GetCategoriesByIdsQueryValidator
        : AbstractValidator<GetCategoriesByIdsQuery>
    {
        public GetCategoriesByIdsQueryValidator()
        {
            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("CategoryIds must not be empty.");
        }
    }
}
