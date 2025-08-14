using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService
{
    public interface IProductPriceCommandService
    {
        // -------------------- [CRUD Operations] --------------------
        Task<Guid> CreatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddPricesRangeAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default);
        Task<bool> UpdatePriceAsync(ProductPrice price, CancellationToken cancellationToken = default);

        // -------------------- [Soft‑Delete Operations] --------------------
        /// <summary>Soft delete a price record.</summary>
        Task<bool> SoftDeletePriceAsync(Guid priceId, CancellationToken cancellationToken = default);

        /// <summary>Soft delete multiple price records.</summary>
        Task<bool> SoftDeletePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);

        /// <summary>Soft delete all price records.</summary>
        Task<bool> SoftDeleteAllPricesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Hard‑Delete Operations] --------------------
        /// <summary>Hard delete a price record permanently.</summary>
        Task<bool> DeletePriceAsync(Guid priceId, CancellationToken cancellationToken = default);

        /// <summary>Hard delete multiple price records permanently.</summary>
        Task<bool> DeletePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);

        /// <summary>Hard delete all price records permanently.</summary>
        Task<bool> DeleteAllPricesAsync(CancellationToken cancellationToken = default);

        // -------------------- [Restore Operations] --------------------
        /// <summary>Restore a soft‑deleted price record.</summary>
        Task<bool> RestorePriceAsync(Guid priceId, CancellationToken cancellationToken = default);

        /// <summary>Restore multiple soft‑deleted price records.</summary>
        Task<bool> RestorePricesRangeAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);

        /// <summary>Restore all soft‑deleted price records.</summary>
        Task<bool> RestoreAllPricesAsync(CancellationToken cancellationToken = default);

       
        // -------------------- [Bulk Operations] --------------------
        /// <summary>Bulk import price records.</summary>
        Task<int> BulkImportPricesAsync(IEnumerable<ProductPrice> prices, CancellationToken cancellationToken = default);

        /// <summary>Hard bulk delete price records.</summary>
        Task<int> BulkDeletePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);

        /// <summary>Soft bulk delete price records.</summary>
        Task<int> BulkSoftDeletePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);

        /// <summary>Bulk restore soft‑deleted price records.</summary>
        Task<int> BulkRestorePricesAsync(IEnumerable<Guid> priceIds, CancellationToken cancellationToken = default);
    }
}
