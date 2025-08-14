using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos
{
    public class ProductUnitConversionSearchDtoValidator
         : AbstractValidator<ProductUnitConversionSearchDto>
    {
        public ProductUnitConversionSearchDtoValidator()
        {
            Include(new BasePageParamDtoValidator());

            RuleFor(x => x.Keyword)
                .NotEmpty().WithMessage("Search keyword is required.")
                .MinimumLength(3).WithMessage("Keyword must be at least 3 characters.");
        }
    }
}
