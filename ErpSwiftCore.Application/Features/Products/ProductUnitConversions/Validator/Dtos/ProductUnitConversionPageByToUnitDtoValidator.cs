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
    public class ProductUnitConversionPageByToUnitDtoValidator
         : AbstractValidator<ProductUnitConversionPageByToUnitDto>
    {
        public ProductUnitConversionPageByToUnitDtoValidator(
            IProductUnitConversionValidationService valSvc)
        {
            Include(new BasePageParamDtoValidator());

            RuleFor(x => x.ToUnitId)
                .NotEmpty().WithMessage("ToUnitId is required.")
                .MustAsync(valSvc.IsValidUnitAsync)
                    .WithMessage("To unit does not exist or is soft-deleted.");
        }
    }

}
