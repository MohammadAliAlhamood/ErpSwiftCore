using MediatR;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    public class SoftDeleteCostCenterCommand : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }

        public SoftDeleteCostCenterCommand(Guid centerId) => CenterId = centerId;
    }
}
