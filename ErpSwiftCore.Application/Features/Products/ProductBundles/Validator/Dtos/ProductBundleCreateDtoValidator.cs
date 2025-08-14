using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Dtos
{
    public class ProductBundleCreateDtoValidator : AbstractValidator<ProductBundleCreateDto>
    {
        public ProductBundleCreateDtoValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.ParentProductId)
                .NotEmpty().WithMessage("معرّف المنتج الأب مطلوب.")
                .MustAsync(async (parentId, ct) => await validationService.IsValidParentProductAsync(parentId, ct))
                .WithMessage("المنتج الأب غير موجود أو غير صالح.");

            RuleFor(x => x.ComponentProductId)
                .NotEmpty().WithMessage("معرّف المنتج المكون مطلوب.")
                .MustAsync(async (componentId, ct) => await validationService.IsValidComponentProductAsync(componentId, ct))
                .WithMessage("المنتج المكون غير موجود أو غير صالح.");

            RuleFor(x => x)
                .MustAsync(async (dto, ct) =>
                    await validationService.AreDistinctProductsAsync(dto.ParentProductId, dto.ComponentProductId, ct))
                .WithMessage("لا يجوز أن يكون المنتج الأب والمنتج المكون متماثلين.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("الكمية يجب أن تكون أكبر من الصفر.")
                .MustAsync(async (qty, ct) => await validationService.IsQuantityPositiveAsync(qty, ct))
                .WithMessage("الكمية غير صالحة.");

            When(x => x.UnitOfMeasurementId.HasValue && x.UnitOfMeasurementId != Guid.Empty, () =>
            {
                RuleFor(x => x.UnitOfMeasurementId.Value)
                    .MustAsync(async (unitId, ct) => await validationService.IsValidUnitAsync(unitId, ct))
                    .WithMessage("وحدة القياس غير موجودة أو غير صالحة.");
            });

            RuleFor(x => x)
                .MustAsync(async (dto, ct) =>
                    !await validationService.ExistsBundleComponentAsync(dto.ParentProductId, dto.ComponentProductId, null, ct))
                .WithMessage("باقة تربط نفس المنتج الأب والمكون موجودة مسبقاً.");

            RuleFor(x => x)
                .MustAsync(async (dto, ct) =>
                    !await validationService.ExistsCircularDependencyAsync(dto.ParentProductId, dto.ComponentProductId, ct))
                .WithMessage("هناك علاقة دائرية بين المنتج الأب والمنتج المكون.");
        }
    } 
}