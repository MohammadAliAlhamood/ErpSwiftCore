using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Dtos
{

    public class ProductBundleGetByIdsDtoValidator : AbstractValidator<ProductBundleGetByIdsDto>
    {
        public ProductBundleGetByIdsDtoValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.BundleIds)
                .NotNull().WithMessage("قائمة المعرفات مطلوبة.")
                .Must(ids => ids.Any()).WithMessage("يجب أن تحتوي القائمة على معرّف واحد على الأقل.")
                .ForEach(idRule => idRule
                    .NotEmpty().WithMessage("معرّف باقة لا يمكن أن يكون فارغاً.")
                    .MustAsync(async (bundleId, ct) => await validationService.BundleExistsByIdAsync(bundleId, ct))
                    .WithMessage("إحدى الباقات المحددة غير موجودة."));
        }
    }
}
