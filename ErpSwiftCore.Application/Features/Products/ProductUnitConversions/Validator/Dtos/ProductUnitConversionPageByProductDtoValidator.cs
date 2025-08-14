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
    public class ProductUnitConversionPageByProductDtoValidator
      : AbstractValidator<ProductUnitConversionPageByProductDto>
    {
        public ProductUnitConversionPageByProductDtoValidator(
            IProductUnitConversionValidationService valSvc)
        {
            Include(new BasePageParamDtoValidator());

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(valSvc.IsValidProductAsync)
                    .WithMessage("Product does not exist or is soft-deleted.");
        }
    }


}
