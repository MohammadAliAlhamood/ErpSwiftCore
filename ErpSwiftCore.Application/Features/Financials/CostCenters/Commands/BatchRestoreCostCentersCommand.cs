using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class BatchRestoreCostCentersCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CenterIds { get; }

        public BatchRestoreCostCentersCommand(IEnumerable<Guid> centerIds) => CenterIds = centerIds;
    }
}
