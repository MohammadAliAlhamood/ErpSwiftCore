using MediatR; 
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<APIResponseDto>
    {
        public UpdateCustomerDto Dto { get; }
        public UpdateCustomerCommand(UpdateCustomerDto dto) => Dto = dto;
    }

}
