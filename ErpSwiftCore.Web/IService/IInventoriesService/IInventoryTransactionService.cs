using System;
using System.Collections.Generic;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IInventoryTransactionService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetFirstForInventoryAsync(Guid inventoryId);
        Task<APIResponseDto?> GetLastForInventoryAsync(Guid inventoryId);
        Task<APIResponseDto?> GetByInventoryAsync(Guid inventoryId);
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetByTypeAsync(InventoryTransactionType transactionType);
        Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<APIResponseDto?> GetAffectingBalanceAsync(Guid inventoryId);
        Task<APIResponseDto?> SearchByNotesAsync(string noteTerm);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> SumQuantityByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to);
        Task<APIResponseDto?> GetTurnoverRateAsync(Guid productId, DateTime from, DateTime to);
    }
}
