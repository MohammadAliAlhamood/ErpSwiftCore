using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService
{
    /// <summary>
    /// Command service for ProductUnitConversion entity.
    /// Handles CRUD, soft/hard delete, restore, state management and bulk operations.
    /// </summary>
    public interface IProductUnitConversionCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddUnitConversionsRangeAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default);
        Task<bool> UpdateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllUnitConversionsAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> DeleteUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllUnitConversionsAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default);
        Task<bool> RestoreUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllUnitConversionsAsync(CancellationToken cancellationToken = default);
        Task<int> BulkImportUnitConversionsAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<int> BulkSoftDeleteUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
        Task<int> BulkRestoreUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);
    }
}
