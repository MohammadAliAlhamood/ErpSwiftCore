using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Queries
{
    public class GetCostCenterWithChildrenQuery : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }
        public GetCostCenterWithChildrenQuery(Guid centerId) => CenterId = centerId;
    } 
}
