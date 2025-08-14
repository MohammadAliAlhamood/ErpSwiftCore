using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{
    public class GetProductUnitConversionWithProductQueryValidator
      : AbstractValidator<GetProductUnitConversionWithProductQuery>
    {
        public GetProductUnitConversionWithProductQueryValidator()
        {
            RuleFor(x => x.ConversionId)
                .NotEmpty().WithMessage("ConversionId is required.");
        }
    }

}
