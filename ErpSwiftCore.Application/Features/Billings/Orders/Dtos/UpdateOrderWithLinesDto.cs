 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{
    public class UpdateOrderWithLinesDto
    {
        public UpdateOrderDto Order { get; set; } = default!;
        public IEnumerable<UpdateOrderLineDto>? LinesToAdd { get; set; }
        public IEnumerable<UpdateOrderLineDto>? LinesToUpdate { get; set; }
        public IEnumerable<Guid>? LineIdsToDelete { get; set; }
    }
}