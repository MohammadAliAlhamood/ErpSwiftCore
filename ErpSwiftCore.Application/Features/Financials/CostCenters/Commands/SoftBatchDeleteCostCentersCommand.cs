using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    /// <summary>
    /// حذف منطقي دفعي لمجموعة مراكز تكلفة (Soft batch delete)
    /// </summary>
    public class SoftBatchDeleteCostCentersCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CenterIds { get; }
        public SoftBatchDeleteCostCentersCommand(IEnumerable<Guid> centerIds)
            => CenterIds = centerIds;
    }
    }
