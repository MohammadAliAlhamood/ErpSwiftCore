using FluentValidation; 
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Validator.Dtos
{
    public class ProductUnitConversionCreateDtoValidator
        : AbstractValidator<ProductUnitConversionCreateDto>
    {
        [Obsolete]
        public ProductUnitConversionCreateDtoValidator(
            IProductUnitConversionValidationService valSvc)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .MustAsync(valSvc.IsValidProductAsync)
                    .WithMessage("Product does not exist or is soft-deleted.");

            RuleFor(x => x.FromUnitId)
                .NotEmpty().WithMessage("FromUnitId is required.")
                .MustAsync(valSvc.IsValidUnitAsync)
                    .WithMessage("From unit does not exist or is soft-deleted.");

            RuleFor(x => x.ToUnitId)
                .NotEmpty().WithMessage("ToUnitId is required.")
                .MustAsync(valSvc.IsValidUnitAsync)
                    .WithMessage("To unit does not exist or is soft-deleted.");

            RuleFor(x => x)
                .Must(x => x.FromUnitId != x.ToUnitId)
                    .WithMessage("From and To units must be different.");

            RuleFor(x => x)
                .MustAsync((x, ct) => valSvc.IsNotReverseConversionExistsAsync(
                    x.ProductId, x.FromUnitId, x.ToUnitId, ct))
                .WithMessage("A reverse conversion already exists.");

            RuleFor(x => x.ConversionRate)
                .GreaterThan(0).WithMessage("ConversionRate must be greater than zero.")
                .MustAsync((rate, ct) => valSvc.IsValidConversionRateAsync(rate, ct))
                    .WithMessage("ConversionRate is invalid.");

            RuleFor(x => x.Factor)
                .GreaterThan(0).WithMessage("Factor must be greater than zero.")
                .MustAsync((factor, ct) => valSvc.IsValidFactorAsync(factor, ct))
                    .WithMessage("Factor is invalid.");
        }
    }

 

  

  
   

}