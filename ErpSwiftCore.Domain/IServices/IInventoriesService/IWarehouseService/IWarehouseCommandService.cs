using ErpSwiftCore.Domain.Entities.EntityInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService
{
    public interface IWarehouseCommandService
    {
        Task<int> BulkImportWarehousesAsync(IEnumerable<Warehouse> warehouses, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteWarehousesAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default);
        Task<Guid> CreateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddWarehousesRangeAsync(IEnumerable<Warehouse> warehouses, CancellationToken cancellationToken = default);
        Task<bool> UpdateWarehouseAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<bool> DeleteWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<int> DeleteWarehousesRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllWarehousesAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken = default);
        Task<bool> RestoreWarehousesRangeAsync(IEnumerable<Guid> warehouseIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllWarehousesAsync(CancellationToken cancellationToken = default);
    
    }
}
