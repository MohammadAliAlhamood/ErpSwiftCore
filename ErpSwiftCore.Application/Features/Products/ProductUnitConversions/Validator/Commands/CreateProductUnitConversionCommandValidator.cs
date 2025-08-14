using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Commands
{
    public class CreateProductUnitConversionCommandValidator
       : AbstractValidator<CreateProductUnitConversionCommand>
    {
        public CreateProductUnitConversionCommandValidator(
            ProductUnitConversionCreateDtoValidator dtoValidator)
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Payload is required.")
                .SetValidator(dtoValidator);
        }
    } 
} 