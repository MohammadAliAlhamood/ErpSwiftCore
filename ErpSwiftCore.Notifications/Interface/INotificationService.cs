using ErpSwiftCore.Domain.Entities.EntityNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications.Interface
{
    public interface INotificationService
    {
        /// <summary>Create, persist, raise domain event.</summary>
        Task<Guid> CreateAsync(
            Notification notification,
            CancellationToken cancellationToken = default);

        /// <summary>Schedule delivery for future.</summary>
        Task<bool> ScheduleAsync(
            Guid notificationId,
            DateTime scheduledAtUtc,
            CancellationToken cancellationToken = default);

        /// <summary>Send immediately (or retry on failure).</summary>
        Task<bool> SendAsync(
            Guid notificationId,
            CancellationToken cancellationToken = default);

        /// <summary>Mark read.</summary>
        Task<bool> MarkAsReadAsync(
            Guid notificationId,
            CancellationToken cancellationToken = default);
    }

}
