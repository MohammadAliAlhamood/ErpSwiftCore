using MediatR; 
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{ 
    public class CreateCostCenterCommand : IRequest<APIResponseDto>
    {
        public CreateCostCenterDto Dto { get; } 
        public CreateCostCenterCommand(CreateCostCenterDto dto) => Dto = dto;
    }  
}