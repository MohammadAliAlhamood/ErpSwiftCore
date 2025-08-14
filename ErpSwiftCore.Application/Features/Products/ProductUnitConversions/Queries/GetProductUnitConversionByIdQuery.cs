using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries
{
    public class GetProductUnitConversionByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ConversionId { get; }

        public GetProductUnitConversionByIdQuery(Guid conversionId)
        {
            ConversionId = conversionId;
        }
    } 
}