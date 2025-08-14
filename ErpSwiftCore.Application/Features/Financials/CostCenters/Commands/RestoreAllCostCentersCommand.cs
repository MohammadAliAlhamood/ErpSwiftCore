using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class RestoreAllCostCentersCommand : IRequest<APIResponseDto>
    {
        public RestoreAllCostCentersCommand() { }
    }
}
