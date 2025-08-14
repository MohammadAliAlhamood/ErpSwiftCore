using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Queries
{
    // 1. Get by ID
    public class GetAdjustmentByIdQuery : IRequest<APIResponseDto>
    {
        public Guid AdjustmentId { get; }
        public GetAdjustmentByIdQuery(Guid adjustmentId) => AdjustmentId = adjustmentId;
    }

    // 2. Get all (non-deleted)
    public class GetAllAdjustmentsQuery : IRequest<APIResponseDto> { }

    // 3. Get all soft‑deleted
    public class GetAllSoftDeletedAdjustmentsQuery : IRequest<APIResponseDto> { }

    // 4. Get soft‑deleted by ID
    public class GetSoftDeletedAdjustmentByIdQuery : IRequest<APIResponseDto>
    {
        public Guid AdjustmentId { get; }
        public GetSoftDeletedAdjustmentByIdQuery(Guid adjustmentId) => AdjustmentId = adjustmentId;
    }

    // 5. Get by multiple IDs
    public class GetAdjustmentsByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> AdjustmentIds { get; }
        public GetAdjustmentsByIdsQuery(IEnumerable<Guid> adjustmentIds) => AdjustmentIds = adjustmentIds;
    }

    // 6. Get by product (and optional warehouse)
    public class GetAdjustmentsByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid? WarehouseId { get; }
        public GetAdjustmentsByProductQuery(Guid productId, Guid? warehouseId = null)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 7. Get by warehouse
    public class GetAdjustmentsByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetAdjustmentsByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 8. Get by date‑range (and optional warehouse)
    public class GetAdjustmentsByDateRangeQuery : IRequest<APIResponseDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public Guid? WarehouseId { get; }
        public GetAdjustmentsByDateRangeQuery(DateTime from, DateTime to, Guid? warehouseId = null)
        {
            From = from;
            To = to;
            WarehouseId = warehouseId;
        }
    }

    // 9. Get by stock‑take reference
    public class GetAdjustmentsByStockTakeIdQuery : IRequest<APIResponseDto>
    {
        public Guid StockTakeId { get; }
        public GetAdjustmentsByStockTakeIdQuery(Guid stockTakeId) => StockTakeId = stockTakeId;
    }

    // 10. Get counts grouped by Reason (optional filters)
    public class GetAdjustmentCountsByReasonQuery : IRequest<APIResponseDto>
    {
        public Guid? WarehouseId { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public GetAdjustmentCountsByReasonQuery(Guid? warehouseId = null, DateTime? from = null, DateTime? to = null)
        {
            WarehouseId = warehouseId;
            From = from;
            To = to;
        }
    }

    // 11. Get total count
    public class GetAdjustmentsCountQuery : IRequest<APIResponseDto> { }

    // 12. Get count by Product
    public class GetAdjustmentsCountByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetAdjustmentsCountByProductQuery(Guid productId) => ProductId = productId;
    }

    // 13. Get count by Warehouse
    public class GetAdjustmentsCountByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetAdjustmentsCountByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 14. Get adjustment with Product included
    public class GetAdjustmentWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid AdjustmentId { get; }
        public GetAdjustmentWithProductQuery(Guid adjustmentId) => AdjustmentId = adjustmentId;
    }

    // 15. Get adjustment with Warehouse included
    public class GetAdjustmentWithWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid AdjustmentId { get; }
        public GetAdjustmentWithWarehouseQuery(Guid adjustmentId) => AdjustmentId = adjustmentId;
    }

    // 16. Get last adjustment for product + warehouse
    public class GetLastAdjustmentQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public GetLastAdjustmentQuery(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 17. Get current stock after adjustments
    public class GetCurrentStockAfterAdjustmentsQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public GetCurrentStockAfterAdjustmentsQuery(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 18. Sum quantity change by product within date‑range
    public class SumQuantityChangeByProductAndDateRangeQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public SumQuantityChangeByProductAndDateRangeQuery(Guid productId, DateTime from, DateTime to)
        {
            ProductId = productId;
            From = from;
            To = to;
        }
    }
}