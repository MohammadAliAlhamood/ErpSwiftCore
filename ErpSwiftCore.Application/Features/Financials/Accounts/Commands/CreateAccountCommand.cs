using MediatR;
using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<APIResponseDto>
    {
        public CreateAccountDto Dto { get; }
        public CreateAccountCommand(CreateAccountDto dto) => Dto = dto;
    } 
}