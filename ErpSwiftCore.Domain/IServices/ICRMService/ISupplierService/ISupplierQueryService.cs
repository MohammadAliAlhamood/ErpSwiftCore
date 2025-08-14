using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
namespace ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService
{
    public interface ISupplierQueryService
    {

         Task<Supplier?> GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken = default);
         Task<IReadOnlyList<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken = default);
    }
}