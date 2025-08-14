using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Commands
{
    public class RestoreProductUnitConversionCommandValidator
      : AbstractValidator<RestoreProductUnitConversionCommand>
    {
        public RestoreProductUnitConversionCommandValidator()
        {
            RuleFor(x => x.ConversionId)
                .NotEmpty().WithMessage("ConversionId is required.");
        }
    }



}
