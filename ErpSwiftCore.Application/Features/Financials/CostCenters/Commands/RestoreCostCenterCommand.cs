using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class RestoreCostCenterCommand : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }

        public RestoreCostCenterCommand(Guid centerId) => CenterId = centerId;
    }
}
