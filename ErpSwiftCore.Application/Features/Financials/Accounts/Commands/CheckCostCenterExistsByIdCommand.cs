using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class CheckCostCenterExistsByIdCommand : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }
        public CheckCostCenterExistsByIdCommand(Guid centerId) => CenterId = centerId;
    } 
}
