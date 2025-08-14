using ErpSwiftCore.Domain.Entities.EntityCRM;
namespace ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService
{
    public  interface ISupplierCommandService
    {
        Task<IEnumerable<Guid>> AddSuppliersAsync(IEnumerable<Supplier> suppliers, CancellationToken cancellationToken = default);

        Task<Guid> CreateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task<bool> UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task<bool> DeleteSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default);
        Task<bool> DeleteSuppliersRangeAsync(IEnumerable<Guid> supplierIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllSuppliersAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default);
        Task<bool> RestoreSuppliersRangeAsync(IEnumerable<Guid> supplierIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllSuppliersAsync(CancellationToken cancellationToken = default); 
    }
}
