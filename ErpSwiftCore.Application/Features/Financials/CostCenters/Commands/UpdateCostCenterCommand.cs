using MediatR; 
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class UpdateCostCenterCommand : IRequest<APIResponseDto>
    {
        public UpdateCostCenterDto Dto { get; }

        public UpdateCostCenterCommand(UpdateCostCenterDto dto) => Dto = dto;
    }
}
