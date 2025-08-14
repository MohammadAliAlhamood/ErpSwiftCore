using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos
{
    public class SearchProductTaxByProductNameDtoValidator : AbstractValidator<SearchProductTaxByProductNameDto>
    {
        public SearchProductTaxByProductNameDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required.")
                .MinimumLength(2).WithMessage("ProductName must be at least 2 characters.");
        }
    }
}
