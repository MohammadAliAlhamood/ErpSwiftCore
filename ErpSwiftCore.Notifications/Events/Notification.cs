using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications.Events
{
    public record NotificationCreatedEvent(Guid NotificationId, Guid TenantId);
    public record NotificationScheduledEvent(Guid NotificationId, DateTime ScheduledAtUtc);
    public record NotificationSentEvent(Guid NotificationId, DateTime SentAtUtc);
    public record NotificationDeliveredEvent(Guid NotificationId, DateTime DeliveredAtUtc);
    public record NotificationReadEvent(Guid NotificationId, DateTime ReadAtUtc);
    public record NotificationFailedEvent(Guid NotificationId, string Reason);
}