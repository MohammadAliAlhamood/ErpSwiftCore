using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{ 
    public class RestoreCustomerCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public RestoreCustomerCommand(Guid id) => Id = id;
    }
}
