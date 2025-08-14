using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{ 
    public class BulkDeleteOrdersCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> OrderIds { get; }
        public BulkDeleteOrdersCommand(IEnumerable<Guid> orderIds) => OrderIds = orderIds;
    }
}
