using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Queries
{

    public class GetProductUnitConversionsByProductQueryValidator
        : AbstractValidator<GetProductUnitConversionsByProductQuery>
    {
        public GetProductUnitConversionsByProductQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    }


}
