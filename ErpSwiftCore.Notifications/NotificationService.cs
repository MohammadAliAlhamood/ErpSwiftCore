using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.IRepositories.INotificationRepositories;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Notifications.Events;
using ErpSwiftCore.Notifications.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IMultiTenantUnitOfWork _uow;
        private readonly IEventDispatcher _events;
        private readonly NotificationTransportDelegate _transport;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IMultiTenantUnitOfWork uow,
            IEventDispatcher events,
            NotificationTransportDelegate transport,
            ILogger<NotificationService> logger)
        {
            _uow = uow;
            _events = events;
            _transport = transport;
            _logger = logger;
        }

        public async Task<Guid> CreateAsync(Notification notification, CancellationToken ct = default)
        {
            var id = await _uow.Notification.CreateAsync(notification, ct);
            _logger.LogInformation("Created notification {Id}", id);
            await _events.PublishAsync(new NotificationCreatedEvent(id, notification.TenantID), ct);
            return id;
        }

        public async Task<bool> ScheduleAsync(Guid id, DateTime whenUtc, CancellationToken ct = default)
        {
            // حاول استرجاع الإشعار
            var exists = await _uow.Notification.GetByIdAsync(id, ct);

            if (exists == null)
            {
                // إذا لم يوجد، ننشئ إشعاراً جديداً بجدولة فورية
                var newNotif = new Notification(/* … قيم الخصائص المطلوبة … */);
                // نفترض أن المُنشئ الافتراضي يضع حالته Pending
                await _uow.Notification.CreateAsync(newNotif, ct);
                id = newNotif.ID;
            }

            // الآن نضمن أن هناك إشعاراً موجوداً
            var ok = await _uow.Notification.MarkAsScheduledAsync(id, whenUtc, ct);
            if (!ok) return false;

            await _uow.SaveAsync( );
            _logger.LogInformation("Scheduled notification {Id} at {When}", id, whenUtc);
            await _events.PublishAsync(new NotificationScheduledEvent(id, whenUtc), ct);
            return true;
        }

        public async Task<bool> SendAsync(Guid id, CancellationToken ct = default)
        {
            var ok = await _transport(await _uow.Notification.GetByIdAsync(id, ct));
            if (ok)
            {
                // استخدام المستودع للانتقال للحالة Sent
                await _uow.Notification.MarkAsSentAsync(id, ct);
                await _events.PublishAsync(new NotificationSentEvent(id, DateTime.UtcNow), ct);

                // ثم للتسليم Delivered
                await _uow.Notification.MarkAsDeliveredAsync(id, ct);
                await _events.PublishAsync(new NotificationDeliveredEvent(id, DateTime.UtcNow), ct);
            }
            else
            {
                await _uow.Notification.RetryAsync(id, ct);
                _logger.LogWarning("Notification {Id} retry after failure", id);
                await _events.PublishAsync(new NotificationFailedEvent(id, "Transport failure"), ct);
            }

            await _uow.SaveAsync();
            return ok;
        }

        public async Task<bool> MarkAsReadAsync(Guid id, CancellationToken ct = default)
        {
            var ok = await _uow.Notification.MarkAsReadAsync(id, ct);
            if (ok)
            {
                await _uow.SaveAsync();
                await _events.PublishAsync(new NotificationReadEvent(id, DateTime.UtcNow), ct);
            }
            return ok;
        }
    }
}
