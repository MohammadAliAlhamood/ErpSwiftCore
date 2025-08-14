using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService
{
    public interface IInventoryAdjustmentCommandService
    {
        Task<Guid> CreateManualAdjustmentAsync(
            Guid productId,
            Guid warehouseId,
            int quantityChanged,
            string reason,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteAdjustmentAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<bool> RestoreAdjustmentAsync(Guid adjustmentId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAdjustmentsRangeAsync(
            IEnumerable<Guid> adjustmentIds,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAdjustmentsAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAdjustmentsRangeAsync(
           IEnumerable<Guid> adjustmentIds,
           CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAdjustmentsAsync(CancellationToken cancellationToken = default);
        Task<int> BulkDeleteAdjustmentsAsync(
            IEnumerable<Guid> adjustmentIds,
            CancellationToken cancellationToken = default);

        Task<bool> UpdateAdjustmentsAsync(
            IEnumerable<InventoryAdjustment> adjustments,
            CancellationToken cancellationToken = default);
        Task<bool> UpdateAdjustmentReasonByDateRangeAsync(
               DateTime startDate,
               DateTime endDate,
               string newReason,
               CancellationToken cancellationToken = default);


    }
}
