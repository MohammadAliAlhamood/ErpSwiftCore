using MediatR; 
namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.Queries
{
    // 1. Get all inventories in a warehouse
    public class GetInventoriesByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetInventoriesByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 2. Get total inventory records count in a warehouse
    public class GetTotalInventoriesInWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetTotalInventoriesInWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 3. Get total distinct products count in a warehouse
    public class GetTotalProductsInWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetTotalProductsInWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 4. Get low‑stock items count in a warehouse
    public class GetLowStockCountQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetLowStockCountQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 5. Get overstocked items count in a warehouse
    public class GetOverstockedCountQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetOverstockedCountQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 6. Get average stock level in a warehouse
    public class GetAverageStockLevelQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetAverageStockLevelQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 7. Get recent warehouses (by creation date)
    public class GetRecentWarehousesQuery : IRequest<APIResponseDto>
    {
        public int MaxCount { get; }
        public GetRecentWarehousesQuery(int maxCount = 10) => MaxCount = maxCount;
    }

    // 8. Get inventory count per warehouse (for all warehouses)
    public class GetInventoryCountPerWarehouseQuery : IRequest<APIResponseDto> { }

    // 9. Get total number of warehouses
    public class GetWarehousesCountQuery : IRequest<APIResponseDto> { }

    // 10. Get warehouses count by branch
    public class GetWarehousesCountByBranchQuery : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }
        public GetWarehousesCountByBranchQuery(Guid branchId) => BranchId = branchId;
    }

    // 11. Get warehouse by ID
    public class GetWarehouseByIdQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetWarehouseByIdQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 12. Get soft‑deleted warehouse by ID
    public class GetSoftDeletedWarehouseByIdQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetSoftDeletedWarehouseByIdQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 13. Get all warehouses
    public class GetAllWarehousesQuery : IRequest<APIResponseDto> { }

    // 14. Get operational warehouses
    public class GetOperationalWarehousesQuery : IRequest<APIResponseDto> { }

    // 15. Get storage‑only warehouses
    public class GetStorageOnlyWarehousesQuery : IRequest<APIResponseDto> { }

    // 16. Get warehouses by branch
    public class GetWarehousesByBranchQuery : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }
        public GetWarehousesByBranchQuery(Guid branchId) => BranchId = branchId;
    }

    // 17. Get warehouses by a list of IDs
    public class GetWarehousesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> WarehouseIds { get; }
        public GetWarehousesByIdsQuery(IEnumerable<Guid> warehouseIds) => WarehouseIds = warehouseIds;
    }

    // 18. Get warehouse with branch included
    public class GetWarehouseWithBranchQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetWarehouseWithBranchQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 19. Get warehouse with inventories included
    public class GetWarehouseWithInventoriesQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetWarehouseWithInventoriesQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }
}