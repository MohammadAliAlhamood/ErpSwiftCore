using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.AdjustmentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using static ErpSwiftCore.Web.Models.InventorySystemManagmentModels.AdjustmentModels.UpdateInventoryAdjustmentReasonByDateRangeDto;
namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IAdjustmentService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetAllSoftDeletedAsync();
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetByProductAsync(Guid productId, Guid? warehouseId = null);
        Task<APIResponseDto?> GetByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to, Guid? warehouseId = null);
        Task<APIResponseDto?> GetByStockTakeAsync(Guid stockTakeId);
        Task<APIResponseDto?> GetCountsByReasonAsync(Guid? warehouseId = null, DateTime? from = null, DateTime? to = null);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
        Task<APIResponseDto?> GetCountByWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> GetWithProductAsync(Guid id);
        Task<APIResponseDto?> GetWithWarehouseAsync(Guid id);
        Task<APIResponseDto?> GetLastAdjustmentAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> GetCurrentStockAsync(Guid productId, Guid warehouseId);
        Task<APIResponseDto?> SumQuantityByDateRangeAsync(Guid productId, DateTime from, DateTime to);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateInventoryAdjustmentDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> RestoreAllAsync();
        Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> UpdateRangeAsync(UpdateInventoryAdjustmentsRangeDto dto);
        Task<APIResponseDto?> UpdateReasonAsync(UpdateInventoryAdjustmentReasonByDateRangeDto dto);

        // Validation Commands
        Task<APIResponseDto?> CheckExistsAsync(Guid id);
        Task<APIResponseDto?> CheckExistsOnDateAsync(ExistsForProductWarehouseOnDateDto dto);
        Task<APIResponseDto?> ValidateProductAsync(Guid productId);
        Task<APIResponseDto?> ValidateWarehouseAsync(Guid warehouseId);
        Task<APIResponseDto?> ValidateQuantityAsync(int quantity);
    }
}
