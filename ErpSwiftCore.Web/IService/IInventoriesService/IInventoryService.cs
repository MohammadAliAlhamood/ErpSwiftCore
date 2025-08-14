using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IInventoryService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id);
        Task<APIResponseDto?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> GetSnapshotAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> GetLastRecordAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId);
         Task<APIResponseDto?> GetWithProductAsync(Guid id);
        Task<APIResponseDto?> GetWithWarehouseAsync(Guid id);
        Task<APIResponseDto?> GetWithPolicyAsync(Guid id);
        Task<APIResponseDto?> GetWithTransactionsAsync(Guid id);
        Task<APIResponseDto?> GetWithNotificationsAsync(Guid id);
        Task<APIResponseDto?> GetTransactionsAsync(Guid id);
        Task<APIResponseDto?> GetPolicyAsync(Guid id);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
        Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetLowStockCountAsync(Guid warehouseId);
        Task<APIResponseDto?> GetOverstockedCountAsync(Guid warehouseId);
        Task<APIResponseDto?> GetAvailabilityAsync(Guid productId);
        Task<APIResponseDto?> GetStockSummaryAsync(IEnumerable<Guid> productIds);
        Task<APIResponseDto?> GetTotalAvailableAsync(Guid warehouseId);
        Task<APIResponseDto?> GetTotalReservedAsync(Guid warehouseId);
        Task<APIResponseDto?> CalculateValueAsync(Guid warehouseId);
        Task<APIResponseDto?> GetAverageLevelAsync(Guid warehouseId);
        Task<APIResponseDto?> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to);
        Task<APIResponseDto?> GetCurrentAfterAdjustmentsAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> GetBelowReorderAsync();
        Task<APIResponseDto?> GetAboveMaxAsync();
    }
}
