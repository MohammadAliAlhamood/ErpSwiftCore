using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Queries
{
    public class GetCostCentersByNameQuery : IRequest<APIResponseDto>
    {
        public string Name { get; }
        public GetCostCentersByNameQuery(string name) => Name = name;
    }

}
