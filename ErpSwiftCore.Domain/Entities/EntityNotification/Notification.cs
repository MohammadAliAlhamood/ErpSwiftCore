using ErpSwiftCore.SharedKernel.Base; 
namespace ErpSwiftCore.Domain.Entities.EntityNotification
{
    public class Notification : AuditableEntity
    {
        public Guid RecipientId { get; set; }
        public Guid? SenderId { get; set; }
        public NotificationChannel Channel { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string? PayloadJson { get; set; }
        public NotificationPriority Priority { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int RetryCount { get; set; }
        public DateTime? LastTriedAt { get; set; }
        public string? CorrelationId { get; set; }
        public NotificationStatus Status { get; set; }
    }
    public enum NotificationChannel
    {
        InApp,
        Email,
        Sms,
        Push
    }

    public enum NotificationType
    {
        Alert,
        Reminder,
        Promotion,
        Transactional,
        Custom
    }

    public enum NotificationPriority
    {
        Low,
        Normal,
        High,
        Urgent
    }

    public enum NotificationStatus
    {
        Pending,
        Scheduled,
        Sending,
        Sent,
        Delivered,
        Read,
        Failed
    }

}