using ErpSwiftCore.Domain.Entities.EntityInventory;
using MediatR; 
using System.Linq.Expressions; 
namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Queries
{
    // 1. Get stock transfer by its ID
    public class GetStockTransferByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TransferId { get; }
        public GetStockTransferByIdQuery(Guid transferId) => TransferId = transferId;
    }

    // 2. Get soft‑deleted stock transfer by its ID
    public class GetSoftDeletedStockTransferByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TransferId { get; }
        public GetSoftDeletedStockTransferByIdQuery(Guid transferId) => TransferId = transferId;
    }

    // 3. Get all stock transfers
    public class GetAllStockTransfersQuery : IRequest<APIResponseDto> { }

    // 4. Get active stock transfers
    public class GetActiveStockTransfersQuery : IRequest<APIResponseDto> { }

    // 5. Get soft‑deleted stock transfers
    public class GetSoftDeletedStockTransfersQuery : IRequest<APIResponseDto> { }

    // 6. Get stock transfers by a list of IDs
    public class GetStockTransfersByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> TransferIds { get; }
        public GetStockTransfersByIdsQuery(IEnumerable<Guid> transferIds) => TransferIds = transferIds;
    }

    // 7. Get stock transfers by product
    public class GetStockTransfersByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetStockTransfersByProductQuery(Guid productId) => ProductId = productId;
    }

    // 8. Get stock transfers by source warehouse
    public class GetStockTransfersByFromWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid FromWarehouseId { get; }
        public GetStockTransfersByFromWarehouseQuery(Guid fromWarehouseId) => FromWarehouseId = fromWarehouseId;
    }

    // 9. Get stock transfers by destination warehouse
    public class GetStockTransfersByToWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid ToWarehouseId { get; }
        public GetStockTransfersByToWarehouseQuery(Guid toWarehouseId) => ToWarehouseId = toWarehouseId;
    }

    // 10. Get stock transfers by any warehouse (either from or to)
    public class GetStockTransfersByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetStockTransfersByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 11. Get stock transfers within a date range
    public class GetStockTransfersByDateRangeQuery : IRequest<APIResponseDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }
        public GetStockTransfersByDateRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }

    // 12. Get stock transfers matching a filter expression
    public class GetStockTransfersByFilterQuery : IRequest<APIResponseDto>
    {
        public Expression<Func<StockTransfer, bool>> Filter { get; }
        public GetStockTransfersByFilterQuery(Expression<Func<StockTransfer, bool>> filter) => Filter = filter;
    }

    // 13. Get paged stock transfers
    public class GetStockTransfersPagedQuery : IRequest<APIResponseDto>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public GetStockTransfersPagedQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    // 14. Get paged stock transfers by active status
    public class GetStockTransfersPagedByActiveStatusQuery : IRequest<APIResponseDto>
    {
        public bool IsActive { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public GetStockTransfersPagedByActiveStatusQuery(bool isActive, int pageIndex, int pageSize)
        {
            IsActive = isActive;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    // 15. Get paged stock transfers by product
    public class GetStockTransfersPagedByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public GetStockTransfersPagedByProductQuery(Guid productId, int pageIndex, int pageSize)
        {
            ProductId = productId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    // 16. Get paged stock transfers by warehouse
    public class GetStockTransfersPagedByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public GetStockTransfersPagedByWarehouseQuery(Guid warehouseId, int pageIndex, int pageSize)
        {
            WarehouseId = warehouseId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    // 17. Get stock transfer with its product details
    public class GetStockTransferWithProductQuery : IRequest<APIResponseDto>
    {
        public Guid TransferId { get; }
        public GetStockTransferWithProductQuery(Guid transferId) => TransferId = transferId;
    }

    // 18. Get stock transfer with its warehouse details
    public class GetStockTransferWithWarehousesQuery : IRequest<APIResponseDto>
    {
        public Guid TransferId { get; }
        public GetStockTransferWithWarehousesQuery(Guid transferId) => TransferId = transferId;
    }

    // 19. Get total count of stock transfers
    public class GetStockTransfersCountQuery : IRequest<APIResponseDto> { }

    // 20. Get count of stock transfers by product
    public class GetStockTransfersCountByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetStockTransfersCountByProductQuery(Guid productId) => ProductId = productId;
    }

    // 21. Get count of stock transfers by warehouse
    public class GetStockTransfersCountByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetStockTransfersCountByWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }

    // 22. Get total transferred quantity by product (with optional date range)
    public class GetTotalTransferredQuantityByProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public GetTotalTransferredQuantityByProductQuery(Guid productId, DateTime? from = null, DateTime? to = null)
        {
            ProductId = productId;
            From = from;
            To = to;
        }
    }

    // 23. Get total transferred quantity by warehouse (with optional date range)
    public class GetTotalTransferredQuantityByWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public DateTime? From { get; }
        public DateTime? To { get; }
        public GetTotalTransferredQuantityByWarehouseQuery(Guid warehouseId, DateTime? from = null, DateTime? to = null)
        {
            WarehouseId = warehouseId;
            From = from;
            To = to;
        }
    }

    // 24. Search stock transfers by notes
    public class SearchStockTransfersByNotesQuery : IRequest<APIResponseDto>
    {
        public string NoteTerm { get; }
        public SearchStockTransfersByNotesQuery(string noteTerm) => NoteTerm = noteTerm;
    }

    // 25. Search stock transfers by reference number
    public class SearchStockTransfersByReferenceQuery : IRequest<APIResponseDto>
    {
        public string ReferenceNumber { get; }
        public SearchStockTransfersByReferenceQuery(string referenceNumber) => ReferenceNumber = referenceNumber;
    }

    // 26. Search stock transfers by product name
    public class SearchStockTransfersByProductNameQuery : IRequest<APIResponseDto>
    {
        public string ProductName { get; }
        public SearchStockTransfersByProductNameQuery(string productName) => ProductName = productName;
    }

    // 27. Get last stock transfer for a product
    public class GetLastStockTransferForProductQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public GetLastStockTransferForProductQuery(Guid productId) => ProductId = productId;
    }

    // 28. Get last stock transfer for a warehouse
    public class GetLastStockTransferForWarehouseQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public GetLastStockTransferForWarehouseQuery(Guid warehouseId) => WarehouseId = warehouseId;
    }
}