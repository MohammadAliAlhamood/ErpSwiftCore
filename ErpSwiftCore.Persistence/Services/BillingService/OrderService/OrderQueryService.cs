using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using Microsoft.EntityFrameworkCore;

namespace ErpSwiftCore.Persistence.Services.BillingService.OrderService
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public OrderQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
            => _unitOfWork.Order.GetByIdAsync(orderId, cancellationToken);

        public Task<IReadOnlyList<OrderLine>> GetOrderLinesAsync(Guid orderId, CancellationToken cancellationToken = default)
            => _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);

        public async Task<int> GetOrderLinesCountAsync(Guid orderId, CancellationToken cancellationToken = default)
            => await _unitOfWork.OrderLine.CountAsync(orderId, null, cancellationToken);

        public async Task<IEnumerable<Order>> GetOrdersByIdsAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default)
        {
            var ids = orderIds.ToList();
            var results = new List<Order>();
            foreach (var id in ids)
            {
                var order = await _unitOfWork.Order.GetByIdAsync(id, cancellationToken);
                if (order != null) results.Add(order);
            }
            return results;
        }

        // ─────────────── Fetch all orders including their lines & related data ───────────────
        public async Task<IReadOnlyList<Order>> GetAllOrdersWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            // Assumes the repository supports Include via a queryable
            return await _unitOfWork.Order.GetAllAsync(cancellationToken);
        }
         
        // ─────────────── Fetch orders filtered by status ───────────────
        public async Task<IReadOnlyList<Order>> GetOrdersByStatusAsync(
            OrderStatus status,
            CancellationToken cancellationToken = default)
        {
            // يفترض وجود دالة مقابلة في الريبوزيتوري: GetByStatusAsync
            return await _unitOfWork.Order.GetByStatusAsync(status, cancellationToken);
        }

    }
}
