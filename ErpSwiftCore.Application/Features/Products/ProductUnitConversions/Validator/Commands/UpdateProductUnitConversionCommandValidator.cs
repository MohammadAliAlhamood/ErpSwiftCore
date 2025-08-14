using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Commands
{

    public class UpdateProductUnitConversionCommandValidator
        : AbstractValidator<UpdateProductUnitConversionCommand>
    {
        public UpdateProductUnitConversionCommandValidator(
            ProductUnitConversionUpdateDtoValidator dtoValidator)
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Payload is required.")
                .SetValidator(dtoValidator);
        }
    }


}
