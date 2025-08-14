using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class SoftDeleteAllCostCentersCommand : IRequest<APIResponseDto>
    {
        public SoftDeleteAllCostCentersCommand() { }
    }
}
