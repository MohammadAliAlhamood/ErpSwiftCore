using ErpSwiftCore.Domain.Entities.EntityLogging;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationLogging
{
    /// <summary>
    /// تكوين EF Core لجدول LogEntry في مخطط "logging".
    /// يرث من AuditableEntityConfiguration لضبط الحقول المشتركة مثل TenantID وCreatedAt وSoft Delete.
    /// </summary>
    public class LogEntryConfiguration
        : AuditableEntityConfiguration<LogEntry>,
          IEntityTypeConfiguration<LogEntry>
    {
        public override void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            // استدعاء الإعدادات الأساسية للكيان AuditableEntity:
            base.Configure(builder);

            // اسم الجدول والمخطط
            builder.ToTable("LogEntries", schema: "logging");

            // المفتاح الأساسي مُعرَّف في BaseEntity كـ ID من نوع GUID.
            // إذا أردنا جعل المفتاح من نوع int، يجب تغيير الكيان، لكنه حالياً GUID.

            // OccurredAtUtc: وقت الحدث بالـ UTC
            builder.Property(x => x.OccurredAtUtc)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()")
                   .HasComment("UTC timestamp when the event occurred");

            // Level: enum يتم تخزينه كـ int
            builder.Property(x => x.Level)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Severity level as int");

            // Category: نص قصير لتصنيف السجل
            builder.Property(x => x.Category)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Category or area of the event, e.g. 'Inventory', 'Auth'");

            // Operation: enum يتم تخزينه كـ int
            builder.Property(x => x.Operation)
                   .IsRequired()
                   .HasConversion<int>()
                   .HasComment("Operation type as int");

            // EntityName: اسم الكيان المتأثر
            builder.Property(x => x.EntityName)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasComment("Name of the entity affected, e.g. 'Budget'");

            // EntityKey: معرف الكيان كسلسلة
            builder.Property(x => x.EntityKey)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasComment("Identifier of the affected entity as string");

            // Message: وصف مختصر للحدث
            builder.Property(x => x.Message)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasComment("Short message describing the event");
             
            // الجديد – مناسب لـ SQL Server
            builder.Property(x => x.DetailsJson)
                   .HasColumnType("nvarchar(max)")
                   .HasComment("JSON with additional details (old/new values, context)");


            // CorrelationId: سلسلة لتتبع الطلبات/المعاملات
            builder.Property(x => x.CorrelationId)
                   .HasMaxLength(100)
                   .HasComment("Correlation ID to trace related operations");

            // Source: مصدر الحدث (خدمة، خادم، جهاز)
            builder.Property(x => x.Source)
                   .HasMaxLength(100)
                   .HasComment("Source of the event, e.g. service or machine name");

            // Tag: علامة إضافية أو رمز سياقي صغير
            builder.Property(x => x.Tag)
                   .HasMaxLength(200)
                   .HasComment("Optional tag or small contextual code");

            // إعداد CreatedAt وUpdatedAt وCreatedBy وUpdatedBy وIsDeleted تتم في AuditableEntityConfiguration

            // الفهارس لتحسين الأداء عند استعلام السجلات:
            // استعلامات شائعة: حسب المستأجر والفترة الزمنية
            builder.HasIndex(x => new { x.TenantID, x.OccurredAtUtc })
                   .HasDatabaseName("IX_LogEntry_Tenant_OccurredAt");

            // البحث حسب الكيان و المفتاح
            builder.HasIndex(x => new { x.EntityName, x.EntityKey })
                   .HasDatabaseName("IX_LogEntry_Entity");

            // البحث حسب الفئة أو المستوى
            builder.HasIndex(x => x.Category)
                   .HasDatabaseName("IX_LogEntry_Category");
            builder.HasIndex(x => x.Level)
                   .HasDatabaseName("IX_LogEntry_Level");

            // تتبع CorrelationId
            builder.HasIndex(x => x.CorrelationId)
                   .HasDatabaseName("IX_LogEntry_CorrelationId");

            // يمكن إضافة فهرس على CreatedBy أو Source إذا كانت الاستعلامات تتطلب ذلك
            builder.HasIndex(x => x.CreatedBy)
                   .HasDatabaseName("IX_LogEntry_CreatedBy");
             
        }
    }
}