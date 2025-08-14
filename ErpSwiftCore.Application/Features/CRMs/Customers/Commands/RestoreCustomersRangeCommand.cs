using MediatR; 
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Commands
{ 
    public class RestoreCustomersRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> Ids { get; }
        public RestoreCustomersRangeCommand(IEnumerable<Guid> ids) => Ids = ids;
    }
}
