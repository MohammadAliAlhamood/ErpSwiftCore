using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.IRepositories.INotificationRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Repositories.NotificationRepositories
{
    public class NotificationRepository
        : MultiTenantRepository<Notification>,
          INotificationRepository
    {
        private static readonly Expression<Func<Notification, object>>[] DefaultIncludes =
        {
            // If related entities (e.g. Recipient, Sender) are added later, include them here
        };

        public NotificationRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Notification> includeValidator) : base(db, tenantProvider, userProvider, includeValidator)
        {
        }



        // في NotificationRepository (داخل القسم State Transitions)
        public async Task<bool> MarkAsScheduledAsync(Guid notificationId, DateTime whenUtc, CancellationToken cancellationToken = default)
        {
            return await ChangeStatusAsync(
                notificationId,
                NotificationStatus.Scheduled,
                n => n.ScheduledAt = whenUtc,
                cancellationToken
            );
        }

        #region Create

        public async Task<Guid> CreateAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.AddAsync(notification, autoSave: true, cancellationToken);
                return notification.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken cancellationToken = default)
        {
            var ids = new List<Guid>();
            foreach (var n in notifications ?? Array.Empty<Notification>())
            {
                var id = await CreateAsync(n, cancellationToken);
                if (id != Guid.Empty) ids.Add(id);
            }
            return ids;
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            try
            {
                await base.UpdateAsync(notification, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Soft Delete

        public async Task<bool> SoftDeleteAsync(Guid notificationId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetAsync(x => x.ID == notificationId, cancellationToken: cancellationToken);
                if (entity == null) return false;
                await base.SoftDeleteAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default)
        {
            try
            {
                var ids = notificationIds?.ToList() ?? new List<Guid>();
                if (!ids.Any()) return false;
                var list = await base.GetAllAsync(x => ids.Contains(x.ID), cancellationToken: cancellationToken);
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
                return await SoftDeleteRangeAsync(all.Select(x => x.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete / Restore

        public async Task<bool> DeleteAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => await SoftDeleteAsync(notificationId, cancellationToken);

        public async Task<bool> DeleteRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default)
            => await SoftDeleteRangeAsync(notificationIds, cancellationToken);

        public async Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default)
            => await SoftDeleteAllAsync(cancellationToken);

        public async Task<bool> RestoreAsync(Guid notificationId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await base.GetOneSoftDeletedAsync(x => x.ID == notificationId, cancellationToken);
                if (entity == null) return false;
                await base.RestoreAsync(entity, autoSave: true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RestoreRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default)
        {
            var ok = true;
            foreach (var id in notificationIds ?? Array.Empty<Guid>())
                ok &= await RestoreAsync(id, cancellationToken);
            return ok;
        }

        public async Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var deleted = await base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);
                return await RestoreRangeAsync(deleted.Select(x => x.ID), cancellationToken);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region State Transitions

        public async Task<bool> MarkAsSentAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => await ChangeStatusAsync(notificationId, NotificationStatus.Sent, n => n.SentAt = DateTime.UtcNow, cancellationToken);

        public async Task<bool> MarkAsDeliveredAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => await ChangeStatusAsync(notificationId, NotificationStatus.Delivered, n => n.DeliveredAt = DateTime.UtcNow, cancellationToken);

        public async Task<bool> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => await ChangeStatusAsync(notificationId, NotificationStatus.Read, n => n.ReadAt = DateTime.UtcNow, cancellationToken);

        public async Task<bool> RetryAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => await ChangeStatusAsync(notificationId, NotificationStatus.Sending, n =>
            {
                n.RetryCount++;
                n.LastTriedAt = DateTime.UtcNow;
            }, cancellationToken);

        private async Task<bool> ChangeStatusAsync(
              Guid id,
              NotificationStatus newStatus,
              Action<Notification> updateFields,
              CancellationToken ct)
        {
            try
            {
                var entity = await base.GetAsync(x => x.ID == id, ct);
                if (entity == null) return false;

                // apply any timestamp or retry updates
                updateFields(entity);

                // set the status via our helper
                SetStatus(entity, newStatus);

                await base.UpdateAsync(entity, autoSave: true, ct);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the Notification.Status property despite its private setter.
        /// Uses reflection to bypass the private set accessor.
        /// </summary>
        private static void SetStatus(Notification notification, NotificationStatus status)
        {
            var prop = typeof(Notification)
                .GetProperty(nameof(Notification.Status),
                             BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (prop == null || !prop.CanWrite)
                throw new InvalidOperationException("Cannot find or write to Status property on Notification.");

            prop.SetValue(notification, status);
        }






        #endregion











        #region Retrieval

        public Task<Notification?> GetByIdAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => base.GetAsync(x => x.ID == notificationId, cancellationToken, DefaultIncludes);

        public Task<Notification?> GetScheduledNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => base.GetAsync(x => x.ID == notificationId && x.Status == NotificationStatus.Scheduled, cancellationToken, DefaultIncludes);

        public Task<Notification?> GetWithPayloadAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => base.GetAsync(x => x.ID == notificationId && x.PayloadJson != null, cancellationToken, DefaultIncludes);

        public async Task<IReadOnlyList<Notification>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await base.GetAllAsync(null, cancellationToken, DefaultIncludes);
            return list ?? Array.Empty<Notification>();
        }

        public Task<IReadOnlyList<Notification>> GetByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.RecipientId == recipientId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.Status == status, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetByChannelAsync(NotificationChannel channel, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.Channel == channel, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken = default)
            => base.GetAllAsync(x => x.CorrelationId == correlationId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter, CancellationToken cancellationToken = default)
            => base.GetAllAsync(filter, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetSoftDeletedByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default)
            => base.GetAllSoftDeletedAsync(x => x.RecipientId == recipientId, cancellationToken, DefaultIncludes);

        public Task<IReadOnlyList<Notification>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default)
            => base.GetAllSoftDeletedAsync(null, cancellationToken, DefaultIncludes);

        #endregion

        #region Paging & Stats

        public async Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            // Count all notifications
            var total = await base.CountAsync(null, cancellationToken);
            // Retrieve the page, ordered by CreatedAt descending (newest first)
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: null,
                orderBy: x => x.CreatedAt,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes
            );
            return (items, total);
        }
        public async Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetPagedByRecipientAsync(Guid recipientId, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            // Filter by recipient
            Expression<Func<Notification, bool>> filter = x => x.RecipientId == recipientId;
            var total = await base.CountAsync(filter, cancellationToken);
            // Retrieve the page for this recipient, newest first
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.CreatedAt,
                ascending: false,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes
            );
            return (items, total);
        }
        public async Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetScheduledDueAsync(DateTime asOfUtc, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            // Find scheduled notifications that are due
            Expression<Func<Notification, bool>> filter =
                x => x.ScheduledAt.HasValue
                     && x.ScheduledAt.Value <= asOfUtc
                     && x.Status == NotificationStatus.Scheduled;

            var total = await base.CountAsync(filter, cancellationToken);
            // Retrieve the page ordered by ScheduledAt ascending (oldest due first)
            var items = await base.GetPagedAsync(
                pageIndex,
                pageSize,
                filter: filter,
                orderBy: x => x.ScheduledAt!,
                ascending: true,
                cancellationToken: cancellationToken,
                includes: DefaultIncludes
            );
            return (items, total);
        }
        public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
            => base.CountAsync(null, cancellationToken);
        public Task<int> GetCountByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default)
            => base.CountAsync(x => x.RecipientId == recipientId, cancellationToken);
        public Task<int> GetCountByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default)
            => base.CountAsync(x => x.Status == status, cancellationToken);
        #endregion

        #region Search
        public Task<IReadOnlyList<Notification>> SearchByTitleAsync(string titleKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(titleKeyword))
                return Task.FromResult((IReadOnlyList<Notification>)Array.Empty<Notification>());
            var lowered = titleKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.Title.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public Task<IReadOnlyList<Notification>> SearchByMessageAsync(string messageKeyword, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(messageKeyword))
                return Task.FromResult((IReadOnlyList<Notification>)Array.Empty<Notification>());
            var lowered = messageKeyword.ToLowerInvariant();
            return base.GetAllAsync(x => x.Message.ToLower().Contains(lowered), cancellationToken, DefaultIncludes);
        }
        public Task<IReadOnlyList<Notification>> SearchByPayloadFieldAsync(string jsonPath, object value, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<Notification>)Array.Empty<Notification>());
        }
        #endregion
        #region Bulk Delete
        public async Task<int> BulkDeleteAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default)
        {
            var list = await GetByFilterAsync(x => notificationIds.Contains(x.ID), cancellationToken);
            await base.SoftDeleteRangeAsync(list, autoSave: true, cancellationToken);
            return list.Count;
        }
        #endregion
        #region Existence & Validation
        public Task<bool> ExistsAsync(Guid notificationId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(x => x.ID == notificationId, cancellationToken);
        public Task<bool> AnyPendingForRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(x => x.RecipientId == recipientId && x.Status == NotificationStatus.Pending, cancellationToken);
        public Task<bool> IsValidRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default)
            => base.ExistsAsync(x => x.RecipientId == recipientId, cancellationToken);
        #endregion
    }
}
