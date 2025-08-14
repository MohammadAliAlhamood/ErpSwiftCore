using MediatR; 
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{ 
    public class DeleteCustomerCommand : IRequest<APIResponseDto>
    {
        public Guid CustomerID { get; }
        public DeleteCustomerCommand(Guid CustomerId) => CustomerID = CustomerId;
    }

}
