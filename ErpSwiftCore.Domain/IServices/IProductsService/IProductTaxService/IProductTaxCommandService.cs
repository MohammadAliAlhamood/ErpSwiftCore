using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService
{ 
    public interface IProductTaxCommandService
    {
        Task<Guid> CreateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddTaxesRangeAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default);
        Task<bool> UpdateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default);

        // -------------------- [Soft‑Delete Operations] --------------------
        Task<bool> SoftDeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllTaxesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Hard‑Delete Operations] --------------------
        Task<bool> DeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllTaxesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Restore Operations] --------------------
        Task<bool> RestoreTaxAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<bool> RestoreTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllTaxesAsync(CancellationToken cancellationToken = default);

        
        // -------------------- [Bulk Operations] --------------------
        Task<int> BulkImportTaxesAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default);
        Task<int> BulkDeleteTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<int> BulkSoftDeleteTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        Task<int> BulkRestoreTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
    }
}
