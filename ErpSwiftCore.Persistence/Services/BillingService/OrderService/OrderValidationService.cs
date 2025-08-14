using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.OrderService
{
    public class OrderValidationService : IOrderValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public OrderValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> OrderExistsAsync(Guid orderId, CancellationToken cancellationToken = default)
            => _unitOfWork.Order.ExistsAsync(orderId, cancellationToken);

        public Task<bool> OrderLineExistsAsync(Guid orderLineId, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderLine.ExistsAsync(orderLineId, cancellationToken);

        public async Task<bool> HasOrderLinesAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            var lines = await _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);
            return lines.Any();
        }

        public Task<bool> IsOrderLinkedToPartyAsync(Guid orderId, Guid partyId, CancellationToken cancellationToken = default)
            => _unitOfWork.Order.ExistsAsync(o => o.ID == orderId && o.PartyId == partyId, cancellationToken);

        public async Task<bool> ValidateOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            if (!await OrderExistsAsync(orderId, cancellationToken))
                return false;

            var lines = await _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);
            if (!lines.Any()) return false;

            var total = lines.Sum(l => l.SubTotal);
            return total > 0;
        }

        public async Task<decimal> CalculateTotalAmountAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            if (!await OrderExistsAsync(orderId, cancellationToken))
                throw new InvalidOperationException("Order not found.");

            var lines = await _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);
            return lines.Sum(l => l.SubTotal);
        }

        public Task<bool> HasInvoiceForOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
            => _unitOfWork.Invoice.AnyForOrderAsync(orderId, cancellationToken);

        public async Task<bool> CanModifyOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            if (!await OrderExistsAsync(orderId, cancellationToken))
                return false;

            var order = await _unitOfWork.Order.GetByIdAsync(orderId, cancellationToken);
            if (order.OrderStatus == OrderStatus.Completed)
                return false;

            if (await HasInvoiceForOrderAsync(orderId, cancellationToken))
                return false;

            return true;
        }

        public Task<bool> CanDeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
            => CanModifyOrderAsync(orderId, cancellationToken);
    }
}
