using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Queries
{
    public class GetAllUnitsOfMeasurementQuery : IRequest<APIResponseDto>
    {
        public GetAllUnitsOfMeasurementQuery() { }
    }
}