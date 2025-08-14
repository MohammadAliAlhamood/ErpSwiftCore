using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos
{
    public class SearchProductTaxByKeywordDtoValidator : AbstractValidator<SearchProductTaxByKeywordDto>
    {
        public SearchProductTaxByKeywordDtoValidator()
        {
            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("Keyword is required.")
                .MinimumLength(2).WithMessage("Keyword must be at least 2 characters.");
        }
    }

}
