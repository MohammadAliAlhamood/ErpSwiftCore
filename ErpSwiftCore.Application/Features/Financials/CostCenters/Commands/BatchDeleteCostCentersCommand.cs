using MediatR;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    public class BatchDeleteCostCentersCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CenterIds { get; }

        public BatchDeleteCostCentersCommand(IEnumerable<Guid> centerIds) => CenterIds = centerIds;
    }
}

