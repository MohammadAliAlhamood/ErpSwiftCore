using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    public class AddCostCentersRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<CreateCostCenterDto> Centers { get; }

        public AddCostCentersRangeCommand(IEnumerable<CreateCostCenterDto> centers) => Centers = centers;
    }


}
