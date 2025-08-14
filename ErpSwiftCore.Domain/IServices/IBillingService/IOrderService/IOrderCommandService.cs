using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Domain.IServices.IBillingService.IOrderService
{
     public interface IOrderCommandService
    {
        Task<Order> CreateOrderWithLinesAsync(Order order, 
            IEnumerable<OrderLine> orderLines, 
            CancellationToken cancellationToken = default);
        Task<Order> UpdateOrderWithLinesAsync(Order order,
            IEnumerable<OrderLine>? linesToAdd = null, 
            IEnumerable<OrderLine>? linesToUpdate = null,
            IEnumerable<Guid>? lineIdsToDelete = null, 
            CancellationToken cancellationToken = default);
        Task<Order> CreateOrderAsync(
            Order order, 
            CancellationToken cancellationToken = default);
        Task<Order> UpdateOrderAsync(
            Order order, 
            CancellationToken cancellationToken = default);
        Task<bool> DeleteOrderAsync(
            Guid orderId, CancellationToken 
            cancellationToken = default);
        Task<OrderLine> AddOrderLineAsync(
            Guid orderId, 
            OrderLine orderLine, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderLine>> AddOrderLinesAsync(
            Guid orderId, IEnumerable<OrderLine> orderLines, CancellationToken cancellationToken = default);
        Task<OrderLine> UpdateOrderLineAsync(OrderLine orderLine, CancellationToken cancellationToken = default);
        Task<bool> DeleteOrderLineAsync(Guid orderLineId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllLinesOfOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default);
        Task<bool> DeleteOrdersRangeAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default);
    }
}
