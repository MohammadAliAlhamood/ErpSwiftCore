using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{

    public class GetProductUnitConversionsByIdsQueryValidator
        : AbstractValidator<GetProductUnitConversionsByIdsQuery>
    {
        public GetProductUnitConversionsByIdsQueryValidator()
        {
            RuleFor(x => x.ConversionIds)
                .NotNull().WithMessage("ConversionIds list is required.")
                .Must(ids => ids.Any()).WithMessage("At least one ConversionId must be provided.")
                .ForEach(idRule => idRule.NotEmpty().WithMessage("ConversionId cannot be empty."));
        }
    }


}
