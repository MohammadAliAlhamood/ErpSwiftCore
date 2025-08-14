using ErpSwiftCore.Domain.Entities.EntityLogging;
using ErpSwiftCore.Domain.IRepositories.ILoggingRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Repositories.LoggingRepositories
{
    public class LogEntryRepository  : MultiTenantRepository<LogEntry>,   ILogEntryRepository
    {
        // إذا كان هناك جداول مرتبطة بـ LogEntry مستقبلاً، يمكن إضافتها هنا
        private static readonly Expression<Func<LogEntry, object>>[] DefaultIncludes =
        {
            // مثلا: x => x.SomeNavigationProperty
        };

        public LogEntryRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<LogEntry> includeValidator) 
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        #region Soft Delete Operations

        public async Task<bool> SoftDeleteAsync(Guid logEntryId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(x => x.ID == logEntryId, cancellationToken: cancellationToken, includes: DefaultIncludes);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = logEntryIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return false;
                var list = await base.GetAllAsync(x => ids.Contains(x.ID), cancellationToken, DefaultIncludes);
                if (list == null || !list.Any()) return false;
                await base.SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var all = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
                var ids = all.Select(x => x.ID).ToList();
                if (!ids.Any()) return false;
                return await SoftDeleteRangeAsync(ids, cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region CRUD Operations

        public async Task<Guid> CreateAsync(LogEntry logEntry, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(logEntry, autoSave: true, cancellationToken);
                return logEntry.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<LogEntry> logEntries, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var e in logEntries ?? Array.Empty<LogEntry>())
            {
                var id = await CreateAsync(e, cancellationToken);
                if (id != Guid.Empty) ids.Add(id);
            }
            return ids;
        }

        public async Task<bool> UpdateAsync(LogEntry logEntry, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(logEntry, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete / Restore

        public async Task<bool> DeleteAsync(Guid logEntryId, CancellationToken cancellationToken = default)
            => await SoftDeleteAsync(logEntryId, cancellationToken);

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default)
            => await SoftDeleteRangeAsync(logEntryIds, cancellationToken);

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
            => await SoftDeleteAllAsync(cancellationToken);

        public async Task<bool> RestoreAsync(Guid logEntryId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(x => x.ID == logEntryId, cancellationToken);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in logEntryIds ?? Array.Empty<Guid>())
            {
                ok &= await RestoreAsync(id, cancellationToken);
            }
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
                var ids = deleted.Select(x => x.ID).ToList();
                return await RestoreRangeAsync(ids, cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Single Retrieval

        public Task<LogEntry?> GetByIdAsync(Guid logEntryId, CancellationToken cancellationToken = default)
            => base.GetAsync(x => x.ID == logEntryId, cancellationToken: cancellationToken, includes: DefaultIncludes);

        public Task<LogEntry?> GetWithDetailsAsync(Guid logEntryId, CancellationToken cancellationToken = default)
            => base.GetAsync(x => x.ID == logEntryId, cancellationToken: cancellationToken, includes: DefaultIncludes);
        // حالياً لا خصائص مطوّلة، لكن المستقبل قد يتطلب تضمين علاقات

        #endregion

        #region Bulk / Advanced Retrieval

        public async Task<IReadOnlyList<LogEntry>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return list ?? Array.Empty<LogEntry>();
        }

        public Task<IReadOnlyList<LogEntry>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(category))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = category.Trim().ToLower();
            return base.GetAllAsync(x => x.Category.ToLower() == lowered, cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByLevelAsync(LogLevel level, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.Level == level, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<LogEntry>> GetByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(entityKey))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var nameLower = entityName.Trim().ToLower();
            var keyLower = entityKey.Trim().ToLower();
            return base.GetAllAsync(x =>
                x.EntityName.ToLower() == nameLower &&
                x.EntityKey.ToLower() == keyLower,
                cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entityName))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = entityName.Trim().ToLower();
            return base.GetAllAsync(x => x.EntityName.ToLower() == lowered, cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(correlationId))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var trimmed = correlationId.Trim();
            return base.GetAllAsync(x => x.CorrelationId == trimmed, cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.CreatedBy == userId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<LogEntry>> GetBySourceAsync(string source, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(source))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = source.Trim().ToLower();
            return base.GetAllAsync(x => x.Source != null && x.Source.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByTagAsync(string tag, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = tag.Trim().ToLower();
            return base.GetAllAsync(x => x.Tag != null && x.Tag.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default)
        {
            return base.GetAllAsync(x => x.OccurredAtUtc >= fromUtc && x.OccurredAtUtc <= toUtc, cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> GetByFilterAsync(Expression<Func<LogEntry, bool>> filter, CancellationToken cancellationToken = default)
            => base.GetAllAsync(filter, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<LogEntry>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
            => base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<LogEntry>> GetSoftDeletedByUserAsync(Guid userId, CancellationToken cancellationToken = default)
            => base.GetAllSoftDeletedAsync(x => x.CreatedBy == userId, cancellationToken, DefaultIncludes);

        #endregion

        #region Paging, Filtering & Stats

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var total = await base.CountAsync(null, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: null,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByCategoryAsync(string category, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(category))
                return (Array.Empty<LogEntry>(), 0);

            var lowered = category.Trim().ToLower();
            Expression<Func<LogEntry, bool>> filter = x => x.Category.ToLower() == lowered;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByLevelAsync(LogLevel level, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<LogEntry, bool>> filter = x => x.Level == level;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByEntityAsync(string entityName, string entityKey, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(entityKey))
                return (Array.Empty<LogEntry>(), 0);

            var nameLower = entityName.Trim().ToLower();
            var keyLower = entityKey.Trim().ToLower();
            Expression<Func<LogEntry, bool>> filter = x =>
                x.EntityName.ToLower() == nameLower &&
                x.EntityKey.ToLower() == keyLower;

            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByUserAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<LogEntry, bool>> filter = x => x.CreatedBy == userId;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public async Task<(IReadOnlyList<LogEntry> Items, int TotalCount)> GetPagedByDateRangeAsync(DateTime fromUtc, DateTime toUtc, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            Expression<Func<LogEntry, bool>> filter = x => x.OccurredAtUtc >= fromUtc && x.OccurredAtUtc <= toUtc;
            var total = await base.CountAsync(filter, cancellationToken);
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.OccurredAtUtc,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes);
            return (items, total);
        }

        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);

        public Task<int> GetCountByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(category))
                return Task.FromResult(0);
            var lowered = category.Trim().ToLower();
            return base.CountAsync(x => x.Category.ToLower() == lowered, cancellationToken);
        }

        public Task<int> GetCountByLevelAsync(LogLevel level, CancellationToken cancellationToken = default)
            => base.CountAsync(x => x.Level == level, cancellationToken);

        public Task<int> GetCountByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(entityKey))
                return Task.FromResult(0);
            var nameLower = entityName.Trim().ToLower();
            var keyLower = entityKey.Trim().ToLower();
            return base.CountAsync(x => x.EntityName.ToLower() == nameLower && x.EntityKey.ToLower() == keyLower, cancellationToken);
        }

        public Task<int> GetCountByUserAsync(Guid userId, CancellationToken cancellationToken = default)
            => base.CountAsync(x => x.CreatedBy == userId, cancellationToken);

        public Task<int> GetCountByDateRangeAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default)
            => base.CountAsync(x => x.OccurredAtUtc >= fromUtc && x.OccurredAtUtc <= toUtc, cancellationToken);

        #endregion

        #region Search Operations

        public Task<IReadOnlyList<LogEntry>> SearchByMessageAsync(string messageKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(messageKeyword))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = messageKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.Message.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> SearchByDetailsAsync(string detailsKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(detailsKeyword))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = detailsKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.DetailsJson != null && x.DetailsJson.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> SearchBySourceAsync(string sourceKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sourceKeyword))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = sourceKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.Source != null && x.Source.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        public Task<IReadOnlyList<LogEntry>> SearchByTagAsync(string tagKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(tagKeyword))
                return Task.FromResult((IReadOnlyList<LogEntry>)Array.Empty<LogEntry>());

            var lowered = tagKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.Tag != null && x.Tag.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }

        #endregion

        #region Bulk Delete

        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> logEntryIds, CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(x => logEntryIds.Contains(x.ID), cancellationToken, DefaultIncludes);
            if (list == null || !list.Any()) return 0;
            await base.SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return list.Count;
        }

        #endregion

        #region Existence & Validation

        public Task<bool> ExistsAsync(Guid logEntryId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(x => x.ID == logEntryId, cancellationToken);

        public Task<bool> AnyByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(category))
                return Task.FromResult(false);
            var lowered = category.Trim().ToLower();
            return base.ExistsAsync(x => x.Category.ToLower() == lowered, cancellationToken);
        }

        public Task<bool> AnyByEntityAsync(string entityName, string entityKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entityName) || string.IsNullOrWhiteSpace(entityKey))
                return Task.FromResult(false);
            var nameLower = entityName.Trim().ToLower();
            var keyLower = entityKey.Trim().ToLower();
            return base.ExistsAsync(x =>
                x.EntityName.ToLower() == nameLower &&
                x.EntityKey.ToLower() == keyLower,
                cancellationToken);
        }

        #endregion
    }
}







