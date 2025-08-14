using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService
{
    public interface IProductTaxQueryService
    { 
        #region Single Retrieval
        Task<ProductTax?> GetTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        Task<ProductTax?> GetSoftDeletedTaxByIdAsync(Guid taxId, CancellationToken cancellationToken = default);
        #endregion
        #region Bulk / Advanced Retrieval
        Task<IReadOnlyList<ProductTax>> GetAllTaxesAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<ProductTax>> GetSoftDeletedTaxesAsync(CancellationToken cancellationToken = default); 
        Task<IReadOnlyList<ProductTax>> GetTaxesByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductTax>> GetTaxesByIdsAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default);
        #endregion

  

        #region Relations
        Task<ProductTax?> GetTaxWithProductAsync(Guid taxId, CancellationToken cancellationToken = default);
        #endregion

        #region Counts & Stats
        Task<int> GetTaxesCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetTaxesCountByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<int> GetSoftDeletedTaxesCountAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}
