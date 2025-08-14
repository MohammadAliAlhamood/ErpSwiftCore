using ErpSwiftCore.Application.Features.Products.Products.Validators;
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
    public class BulkImportProductUnitConversionsCommandValidator
      : AbstractValidator<BulkImportProductUnitConversionsCommand>
    {
        public BulkImportProductUnitConversionsCommandValidator(
            ProductUnitConversionCreateDtoValidator dtoValidator)
        {
            RuleFor(x => x.Dtos)
                .NotNull().WithMessage("List of conversions is required.")
                .Must(list => list.Any()).WithMessage("At least one conversion must be provided.")
                .ForEach(rule => rule.SetValidator(dtoValidator));
        }
    }


}
