using MediatR; 
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{ 
    public class CreateCustomerCommand : IRequest<APIResponseDto>
    {
        public CreateCustomerDto Dto { get; }
        public CreateCustomerCommand(CreateCustomerDto dto) => Dto = dto;
    } 
}
