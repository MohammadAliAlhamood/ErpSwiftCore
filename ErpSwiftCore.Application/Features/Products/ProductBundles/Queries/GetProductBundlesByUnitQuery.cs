using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Queries
{
    public class GetProductBundlesByUnitQuery : IRequest<APIResponseDto>
    {
        public Guid UnitOfMeasurementId { get; }
        public GetProductBundlesByUnitQuery(Guid unitOfMeasurementId)
        {
            UnitOfMeasurementId = unitOfMeasurementId;
        }
    }
}
