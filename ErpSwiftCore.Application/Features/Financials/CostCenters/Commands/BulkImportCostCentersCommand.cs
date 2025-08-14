using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    public class BulkImportCostCentersCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<CreateCostCenterDto> Centers { get; }

        public BulkImportCostCentersCommand(IEnumerable<CreateCostCenterDto> centers) => Centers = centers;
    }
}
