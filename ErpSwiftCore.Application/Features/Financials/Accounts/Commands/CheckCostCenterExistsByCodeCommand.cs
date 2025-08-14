using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class CheckCostCenterExistsByCodeCommand : IRequest<APIResponseDto>
    {
        public string Code { get; }
        public CheckCostCenterExistsByCodeCommand(string code) => Code = code;
    } 
}
