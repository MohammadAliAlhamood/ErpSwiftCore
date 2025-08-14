using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots; 
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions; 
namespace ErpSwiftCore.Persistence.Repositories.InventoryRepositories
{
    public class InventoryTransactionRepository
        : MultiTenantRepository<InventoryTransaction>, IInventoryTransactionRepository
    {
        private static readonly Expression<Func<InventoryTransaction, object>>[] DefaultIncludes =  {  x => x.Inventory  };

        public InventoryTransactionRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<InventoryTransaction> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(InventoryTransaction tx, CancellationToken cancellationToken = default)
        {
            await base.AddAsync(tx, true, cancellationToken);
            return tx.ID;
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<InventoryTransaction> transactions, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var tx in transactions)
                ids.Add(await CreateAsync(tx, cancellationToken));
            return ids;
        }
        public async Task<bool> UpdateAsync(InventoryTransaction tx, CancellationToken cancellationToken = default)
        {
            await base.UpdateAsync(tx, true, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteAsync(Guid txId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetAsync(t => t.ID == txId, cancellationToken: cancellationToken);
            if (entity is null) return false;
            await base.SoftDeleteAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> txIds, CancellationToken cancellationToken = default)
        {
            var items = await GetByIdsAsync(txIds, cancellationToken);
            await base.SoftDeleteRangeAsync(items, true, cancellationToken);
            return items.Count;
        }
        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await GetAllAsync(cancellationToken);
            await base.SoftDeleteRangeAsync(all, true, cancellationToken);
            return true;
        }
        public async Task<bool> RestoreAsync(Guid txId, CancellationToken cancellationToken = default)
        {
            var entity = await base.GetOneSoftDeletedAsync(t => t.ID == txId, cancellationToken);
            if (entity is null) return false;
            await base.RestoreAsync(entity, true, cancellationToken);
            return true;
        }
        public async Task<int> BulkRestoreAsync(IEnumerable<Guid> txIds, CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            var toRestore = SoftDeleted.Where(t => txIds.Contains(t.ID)).ToList();
            await base.RestoreRangeAsync(toRestore, true, cancellationToken);
            return toRestore.Count;
        }
        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            var SoftDeleted = await GetSoftDeletedAsync(cancellationToken);
            await base.RestoreRangeAsync(SoftDeleted, true, cancellationToken);
            return true;
        }
        public Task<bool> ExistsByIdAsync(Guid txId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(t => t.ID == txId, cancellationToken);
        public Task<bool> ExistsForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(t => t.InventoryID == inventoryId, cancellationToken);
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
            => await base.CountAsync(null, cancellationToken);
        public async Task<InventoryTransaction?> GetByIdAsync(Guid txId, CancellationToken cancellationToken = default)
            => await base.GetAsync(t => t.ID == txId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByIdsAsync(IEnumerable<Guid> txIds, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => txIds.Contains(t.ID), cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetAllAsync(CancellationToken cancellationToken = default)
            => await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            => await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
        public Task<InventoryTransaction?> GetSoftDeletedByIdAsync(Guid txId, CancellationToken cancellationToken = default)
            => base.GetOneSoftDeletedAsync(t => t.ID == txId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByFilterAsync(Expression<Func<InventoryTransaction, bool>> filter, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(filter, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.TransactionDate >= from && t.TransactionDate <= to, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByTypeAsync(InventoryTransactionType type, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.TransactionType == type, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByInventoryIdAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.InventoryID == inventoryId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.Inventory.ProductID == productId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.Inventory.WarehouseID == warehouseId, cancellationToken, DefaultIncludes);
        public async Task<IReadOnlyList<InventoryTransaction>> GetByJournalEntryIdAsync(Guid journalEntryId, CancellationToken cancellationToken = default)
            => await base.GetAllAsync(t => t.RelatedJournalEntryID == journalEntryId, cancellationToken, DefaultIncludes);
        public async Task<InventoryTransaction?> GetFirstTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => (await base.GetAllAsync(t => t.InventoryID == inventoryId, cancellationToken, DefaultIncludes))
               .OrderBy(t => t.TransactionDate)
               .FirstOrDefault();
        public async Task<InventoryTransaction?> GetLastTransactionForInventoryAsync(Guid inventoryId, CancellationToken cancellationToken = default)
            => (await base.GetAllAsync(t => t.InventoryID == inventoryId, cancellationToken, DefaultIncludes))
               .OrderByDescending(t => t.TransactionDate)
               .FirstOrDefault();
        public async Task<(IReadOnlyList<InventoryTransaction> Transactions, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(null, cancellationToken);
            var items = await base.GetPagedAsync(pageIndex, pageSize, null, t => t.TransactionDate, false, cancellationToken, DefaultIncludes);
            return (items, total);
        }
        public async Task<IReadOnlyList<InventoryTransaction>> SearchByNotesAsync(string noteTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(noteTerm)) return Array.Empty<InventoryTransaction>();
            var lowered = noteTerm.ToLower();
            return await base.GetAllAsync(t => t.Notes != null && t.Notes.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<InventoryTransaction>> SearchByReferenceAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return Array.Empty<InventoryTransaction>();
            var lowered = searchTerm.ToLower();
            return await base.GetAllAsync(t => t.ReferenceNumber != null && t.ReferenceNumber.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public async Task<IReadOnlyList<InventoryTransaction>> GetTransactionsAffectingBalanceAsync(Guid inventoryId, CancellationToken cancellationToken = default)
        {
            var transactions = await base.GetAllAsync(t => t.InventoryID == inventoryId, cancellationToken, DefaultIncludes);
            return transactions.OrderBy(t => t.TransactionDate).ToList();
        }
        public async Task<int> SumQuantityChangeByProductAndDateRangeAsync(Guid productId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            // جلب جميع المعاملات المرتبطة بالمنتج
            var transactions = await GetByProductIdAsync(productId, cancellationToken);
            var sum = transactions.Where(tx => tx.TransactionDate >= from && tx.TransactionDate <= to).Sum(tx => tx.Quantity);
            return sum;
        }
    }
}