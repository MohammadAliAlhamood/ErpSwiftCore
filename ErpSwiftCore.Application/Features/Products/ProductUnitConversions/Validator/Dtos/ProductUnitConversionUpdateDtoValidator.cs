using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos
{
    public class ProductUnitConversionUpdateDtoValidator
      : AbstractValidator<ProductUnitConversionUpdateDto>
    {
        [Obsolete]
        public ProductUnitConversionUpdateDtoValidator(
            IProductUnitConversionValidationService valSvc)
        {
            Include(new ProductUnitConversionCreateDtoValidator(valSvc));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Conversion Id is required.")
                .MustAsync(valSvc.UnitConversionExistsByIdAsync)
                    .WithMessage("Conversion does not exist.");
        }
    }


}
