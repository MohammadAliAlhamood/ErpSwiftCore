using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class DeleteAllCostCentersCommand : IRequest<APIResponseDto>
    {
        public DeleteAllCostCentersCommand() { }
    }
}
