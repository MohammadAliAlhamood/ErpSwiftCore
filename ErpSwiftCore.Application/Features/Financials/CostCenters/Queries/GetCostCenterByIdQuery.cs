using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Queries
{
    public class GetCostCenterByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }
        public GetCostCenterByIdQuery(Guid centerId) => CenterId = centerId;
    } 
}