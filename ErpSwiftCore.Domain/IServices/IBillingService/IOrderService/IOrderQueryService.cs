using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
namespace ErpSwiftCore.Domain.IServices.IBillingService.IOrderService
{
    public interface IOrderQueryService
    {
        Task<int> GetOrderLinesCountAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByIdsAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<OrderLine>> GetOrderLinesAsync(Guid orderId, CancellationToken cancellationToken = default);






        Task<IReadOnlyList<Order>> GetAllOrdersWithDetailsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default);
    }
}
