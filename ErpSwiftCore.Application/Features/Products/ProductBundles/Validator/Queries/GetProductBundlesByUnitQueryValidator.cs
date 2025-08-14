using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using FluentValidation; 

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Validator.Queries
{
    public class GetProductBundlesByUnitQueryValidator 
        : AbstractValidator<GetProductBundlesByUnitQuery>
    {
        public GetProductBundlesByUnitQueryValidator(IProductBundleValidationService validationService)
        {
            RuleFor(x => x.UnitOfMeasurementId)
                .NotEmpty().WithMessage("UnitOfMeasurementId is required.")
                .MustAsync(async (id, ct) => await validationService.IsValidUnitAsync(id, ct))
                .WithMessage("Specified unit of measurement does not exist or is invalid.");
        }
    }


}
