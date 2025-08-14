using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{

    public class GetProductUnitConversionsByToUnitQueryValidator
        : AbstractValidator<GetProductUnitConversionsByToUnitQuery>
    {
        public GetProductUnitConversionsByToUnitQueryValidator()
        {
            RuleFor(x => x.ToUnitId)
                .NotEmpty().WithMessage("ToUnitId is required.");
        }
    }



}
