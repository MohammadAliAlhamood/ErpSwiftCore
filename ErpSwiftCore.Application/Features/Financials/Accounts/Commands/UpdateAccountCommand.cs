using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class UpdateAccountCommand : IRequest<APIResponseDto>
    {
        public UpdateAccountDto Dto { get; }
        public UpdateAccountCommand(UpdateAccountDto dto) => Dto = dto;
    }

}
