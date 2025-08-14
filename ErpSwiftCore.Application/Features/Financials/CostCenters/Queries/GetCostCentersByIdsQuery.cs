using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Queries
{
    public class GetCostCentersByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CenterIds { get; }
        public GetCostCentersByIdsQuery(IEnumerable<Guid> centerIds) => CenterIds = centerIds;
    }
}
