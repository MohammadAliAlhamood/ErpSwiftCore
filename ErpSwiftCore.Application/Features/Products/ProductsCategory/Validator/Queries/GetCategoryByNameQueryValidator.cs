using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Queries
{
    public class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
    {
        public GetCategoryByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
