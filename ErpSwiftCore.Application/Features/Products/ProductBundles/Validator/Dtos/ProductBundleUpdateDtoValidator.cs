using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Dtos
{
    public class ProductBundleUpdateDtoValidator : AbstractValidator<ProductBundleUpdateDto>
    {
        public ProductBundleUpdateDtoValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("معرّف الباقة مطلوب للتحديث.")
                .MustAsync(async (id, ct) => await validationService.BundleExistsByIdAsync(id, ct))
                .WithMessage("الباقة المحددة غير موجودة.");

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
                    !await validationService.ExistsBundleComponentAsync(dto.ParentProductId, dto.ComponentProductId, dto.ID, ct))
                .WithMessage("باقة تربط نفس المنتج الأب والمكون موجودة مسبقاً.");

            RuleFor(x => x)
                .MustAsync(async (dto, ct) =>
                    !await validationService.ExistsCircularDependencyAsync(dto.ParentProductId, dto.ComponentProductId, ct))
                .WithMessage("هناك علاقة دائرية بين المنتج الأب والمنتج المكون.");
        }
    }

}
