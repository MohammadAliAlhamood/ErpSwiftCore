using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos
{
    public class ProductUnitConversionPageByFromUnitDtoValidator
      : AbstractValidator<ProductUnitConversionPageByFromUnitDto>
    {
        public ProductUnitConversionPageByFromUnitDtoValidator(
            IProductUnitConversionValidationService valSvc)
        {
            Include(new BasePageParamDtoValidator());

            RuleFor(x => x.FromUnitId)
                .NotEmpty().WithMessage("FromUnitId is required.")
                .MustAsync(valSvc.IsValidUnitAsync)
                    .WithMessage("From unit does not exist or is soft-deleted.");
        }
    }
}
