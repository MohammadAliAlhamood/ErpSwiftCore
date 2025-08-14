using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Queries
{
    public class SearchProductTaxesByProductNameQueryValidator : AbstractValidator<SearchProductTaxesByProductNameQuery>
    {
        public SearchProductTaxesByProductNameQueryValidator()
        {
            RuleFor(x => x.Filter)
                .NotNull().WithMessage("Filter is required.")
                .SetValidator(new SearchProductTaxByProductNameDtoValidator());
        }
    }
}
