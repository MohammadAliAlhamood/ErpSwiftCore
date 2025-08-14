using MediatR;  
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{ 
    public class DeleteCustomersRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> Ids { get; }
        public DeleteCustomersRangeCommand(IEnumerable<Guid> ids) => Ids = ids;
    }
}
