using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using ErpSwiftCore.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationNotification
{
    public class NotificationConfiguration
        : AuditableEntityConfiguration<Notification>,
          IEntityTypeConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.ToTable("Notifications", schema: "notification");

            // Required fields
            builder.Property(x => x.RecipientId)
                   .IsRequired();

            builder.Property(x => x.SenderId);

            // Enums stored explicitly as int
            builder.Property(x => x.Channel)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Delivery channel as int");

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Notification type as int");

            builder.Property(x => x.Priority)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Priority as int");

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Status as int");

            // Text fields
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Message)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(x => x.PayloadJson)
                   .HasColumnType("nvarchar(max)")
                   .HasComment("JSON payload with additional data");


            builder.Property(x => x.CorrelationId)
                   .HasMaxLength(100);

            // Date/time tracking
            builder.Property(x => x.ScheduledAt);
            builder.Property(x => x.SentAt);
            builder.Property(x => x.DeliveredAt);
            builder.Property(x => x.ReadAt);
            builder.Property(x => x.ExpiresAt);

            // Retry behavior
            builder.Property(x => x.RetryCount)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(x => x.LastTriedAt);

            // Audit defaults
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => x.RecipientId).HasDatabaseName("IX_Notification_Recipient");
            builder.HasIndex(x => x.Status).HasDatabaseName("IX_Notification_Status");
            builder.HasIndex(x => new { x.RecipientId, x.Status }).HasDatabaseName("IX_Notification_Recipient_Status");
            builder.HasIndex(x => x.ScheduledAt).HasDatabaseName("IX_Notification_ScheduledAt");
            builder.HasIndex(x => x.CorrelationId).HasDatabaseName("IX_Notification_CorrelationId");
        }
    }
}
