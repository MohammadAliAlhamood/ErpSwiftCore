namespace ErpSwiftCore.Domain.IServices.IBillingService.IOrderService
{
    public interface IOrderValidationService
    {
        Task<bool> ValidateOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> OrderExistsAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> OrderLineExistsAsync(Guid orderLineId, CancellationToken cancellationToken = default);
        Task<bool> HasOrderLinesAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<bool> IsOrderLinkedToPartyAsync(Guid orderId, Guid partyId, CancellationToken cancellationToken = default);
        Task<decimal> CalculateTotalAmountAsync(Guid orderId, CancellationToken cancellationToken = default);

         Task<bool> CanModifyOrderAsync(Guid orderId, CancellationToken cancellationToken = default);

         Task<bool> CanDeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<bool> HasInvoiceForOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
