using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{
    public class SearchProductUnitConversionsQueryValidator
      : AbstractValidator<SearchProductUnitConversionsQuery>
    {
        public SearchProductUnitConversionsQueryValidator(
            ProductUnitConversionSearchDtoValidator dtoValidator)
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Payload is required.")
                .SetValidator(dtoValidator);
        }
    }
}
