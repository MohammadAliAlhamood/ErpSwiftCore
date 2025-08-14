using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class CheckCostCenterExistsByNameCommand : IRequest<APIResponseDto>
    {
        public string Name { get; }
        public CheckCostCenterExistsByNameCommand(string name) => Name = name;
    }

}
