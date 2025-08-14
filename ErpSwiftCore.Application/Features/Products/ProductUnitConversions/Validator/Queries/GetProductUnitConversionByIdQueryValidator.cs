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
    // Query Validators

    public class GetProductUnitConversionByIdQueryValidator
        : AbstractValidator<GetProductUnitConversionByIdQuery>
    {
        public GetProductUnitConversionByIdQueryValidator()
        {
            RuleFor(x => x.ConversionId)
                .NotEmpty().WithMessage("ConversionId is required.");
        }
    }

 
 
}
