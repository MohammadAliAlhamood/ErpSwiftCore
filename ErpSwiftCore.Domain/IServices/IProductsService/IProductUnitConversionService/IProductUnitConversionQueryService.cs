using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService
{
    public interface IProductUnitConversionQueryService
    {
        #region Single Retrieval

        /// <summary>
        /// Retrieves a unit conversion by its ID (active or SoftDeleted).
        /// </summary>
        Task<ProductUnitConversion?> GetUnitConversionByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a unit conversion that has been soft‑deleted.
        /// </summary>
        Task<ProductUnitConversion?> GetSoftDeletedUnitConversionByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);

        #endregion
        #region Bulk / Advanced Retrieval

        /// <summary>All unit conversions (active or SoftDeleted).</summary>
        Task<IReadOnlyList<ProductUnitConversion>> GetAllUnitConversionsAsync(CancellationToken cancellationToken = default);
         
        /// <summary>Soft‑deleted unit conversions (IsDeleted=true).</summary>
        Task<IReadOnlyList<ProductUnitConversion>> GetSoftDeletedUnitConversionsAsync(CancellationToken cancellationToken = default);
 
        Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByFromUnitAsync(Guid fromUnitId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByToUnitAsync(Guid toUnitId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductUnitConversion>> GetUnitConversionsByIdsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default);

        #endregion
       
        #region Relations

        /// <summary>Retrieves conversion with its Product.</summary>
        Task<ProductUnitConversion?> GetUnitConversionWithProductAsync(Guid conversionId, CancellationToken cancellationToken = default);

        /// <summary>Retrieves conversion with its FromUnit.</summary>
        Task<ProductUnitConversion?> GetUnitConversionWithFromUnitAsync(Guid conversionId, CancellationToken cancellationToken = default);

        /// <summary>Retrieves conversion with its ToUnit.</summary>
        Task<ProductUnitConversion?> GetUnitConversionWithToUnitAsync(Guid conversionId, CancellationToken cancellationToken = default);

        #endregion
        #region Counts & Stats

        Task<int> GetUnitConversionsCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetUnitConversionsCountByProductAsync(Guid productId, CancellationToken cancellationToken = default); 
        Task<int> GetSoftDeletedUnitConversionsCountAsync(CancellationToken cancellationToken = default);
         

        #endregion
    }
}
