using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Inventories.Queries
{
    // 1. Get inventory by its ID
    public class GetInventoryByIdQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryByIdQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 2. Get soft‑deleted inventory by its ID
    public class GetSoftDeletedInventoryByIdQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetSoftDeletedInventoryByIdQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 3. Get inventory for a product in a specific warehouse
    public class GetInventoryByProductAndWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public GetInventoryByProductAndWarehouseQuery(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 4. Get current inventory snapshot for a product in a warehouse
    public class GetCurrentInventorySnapshotQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public GetCurrentInventorySnapshotQuery(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 5. Get last inventory record for a product in a warehouse
    public class GetLastInventoryRecordQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public GetLastInventoryRecordQuery(Guid productId, Guid warehouseId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
        }
    }

    // 6. Get all inventories
    public class GetAllInventoriesQuery : IRequest<APIResponseDto>
    {
    }

    // 7. Get inventories by product
    public class GetInventoriesByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetInventoriesByProductQuery(Guid productId) => ProductId = productId;
    }

    // 8. Get inventories by warehouse
    public class GetInventoriesByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetInventoriesByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    } 

    // 10. Get inventories matching a filter expression
    public class GetInventoriesByFilterQuery : IRequest<APIResponseDto>
    {
        public Expression<Func<Inventory, bool>> Filter { get; }
        public GetInventoriesByFilterQuery(Expression<Func<Inventory, bool>> filter) => Filter = filter;
    }

    // 11. Get inventory with its product data
    public class GetInventoryWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryWithProductQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 12. Get inventory with its warehouse data
    public class GetInventoryWithWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryWithWarehouseQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 13. Get inventory with its policy
    public class GetInventoryWithPolicyQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryWithPolicyQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 14. Get inventory with its transactions
    public class GetInventoryWithTransactionsQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryWithTransactionsQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 15. Get inventory with its notifications
    public class GetInventoryWithNotificationsQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryWithNotificationsQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 16. Get raw transactions for an inventory
    public class GetInventoryTransactionsQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryTransactionsQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 17. Get policy for an inventory
    public class GetInventoryPolicyQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetInventoryPolicyQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 18. Get total inventories count
    public class GetInventoriesCountQuery : IRequest<APIResponseDto>
    {
    }

    // 19. Get inventories count by product
    public class GetInventoriesCountByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetInventoriesCountByProductQuery(Guid productId) => ProductId = productId;
    }

    // 20. Get inventories count by warehouse
    public class GetInventoriesCountByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetInventoriesCountByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 21. Get low‑stock count in a warehouse
    public class GetLowStockCountQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetLowStockCountQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 22. Get overstocked count in a warehouse
    public class GetOverstockedCountQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetOverstockedCountQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 23. Get product availability across warehouses
    public class GetProductAvailabilityAcrossWarehousesQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetProductAvailabilityAcrossWarehousesQuery(Guid productId) => ProductId = productId;
    }

    // 24. Get stock summary for multiple products
    public class GetStockSummaryByProductQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ProductIds { get; }
        public GetStockSummaryByProductQuery(IEnumerable<Guid> productIds) => ProductIds = productIds;
    }

    // 25. Get total available quantity in a warehouse
    public class GetTotalAvailableQuantityByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetTotalAvailableQuantityByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 26. Get total reserved quantity in a warehouse
    public class GetTotalReservedQuantityByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetTotalReservedQuantityByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 27. Calculate inventory value in a warehouse
    public class CalculateInventoryValueQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public CalculateInventoryValueQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 28. Get average stock level in a warehouse
    public class GetAverageStockLevelQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetAverageStockLevelQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 29. Get turnover rate for a product in a date range
    public class GetTurnoverRateQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public GetTurnoverRateQuery(Guid productId, DateTime from, DateTime to)
        {
            ProductId = productId;
            From = from;
            To = to;
        }
    }

    // 30. Get current stock after adjustments for a product in a warehouse
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

    // 31. Get all inventories below reorder level
    public class GetBelowReorderLevelQuery : IRequest<APIResponseDto>
    {
    }

    // 32. Get all inventories above max stock level
    public class GetAboveMaxStockLevelQuery : IRequest<APIResponseDto>
    {
    }
}