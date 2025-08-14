using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.INotificationRepositories
{
    /// <summary>
    /// Repository interface for managing notifications.
    /// Supports multi-tenancy, soft delete, scheduling, state transitions, bulk operations,
    /// paging, filtering, search, and validation.
    /// </summary>
    public interface INotificationRepository : IMultiTenantRepository<Notification>
    {
        #region Soft Delete Operations

        Task<bool> SoftDeleteAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default);
        Task<bool> SoftDeleteAllAsync(CancellationToken cancellationToken = default);

        #endregion

        #region CRUD Operations

        Task<Guid> CreateAsync(Notification notification, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Notification notification, CancellationToken cancellationToken = default);

        #endregion

        #region Delete / Archive / Restore

        Task<bool> DeleteAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<bool> RestoreAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> RestoreRangeAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllAsync(CancellationToken cancellationToken = default);

        #endregion

        #region State Transitions
        Task<bool> MarkAsScheduledAsync(Guid notificationId, DateTime whenUtc, CancellationToken cancellationToken = default);
        Task<bool> MarkAsSentAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> MarkAsDeliveredAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> RetryAsync(Guid notificationId, CancellationToken cancellationToken = default);

        #endregion

        #region Single Retrieval

        Task<Notification?> GetByIdAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<Notification?> GetScheduledNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<Notification?> GetWithPayloadAsync(Guid notificationId, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk / Advanced Retrieval

        Task<IReadOnlyList<Notification>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetByChannelAsync(NotificationChannel channel, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetByCorrelationIdAsync(string correlationId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Notification>> GetSoftDeletedByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> GetAllSoftDeletedAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Paging, Filtering & Stats

        Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetPagedByRecipientAsync(
            Guid recipientId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<(IReadOnlyList<Notification> Notifications, int TotalCount)> GetScheduledDueAsync(
            DateTime asOfUtc,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<int> GetCountByRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default);
        Task<int> GetCountByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default);

        #endregion

        #region Search Operations

        Task<IReadOnlyList<Notification>> SearchByTitleAsync(string titleKeyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> SearchByMessageAsync(string messageKeyword, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Notification>> SearchByPayloadFieldAsync(string jsonPath, object value, CancellationToken cancellationToken = default);

        #endregion

        #region Bulk Delete

        Task<int> BulkDeleteAsync(IEnumerable<Guid> notificationIds, CancellationToken cancellationToken = default);

        #endregion

        #region Existence & Validation

        Task<bool> ExistsAsync(Guid notificationId, CancellationToken cancellationToken = default);
        Task<bool> AnyPendingForRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default);
        Task<bool> IsValidRecipientAsync(Guid recipientId, CancellationToken cancellationToken = default);

        #endregion
    }
}
