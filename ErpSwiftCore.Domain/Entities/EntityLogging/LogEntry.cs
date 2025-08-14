using ErpSwiftCore.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.Entities.EntityLogging
{
    /// <summary>
    /// سجل العمليات (LogEntry) للنظام متعدد المستأجرين.
    /// يرث من AuditableEntity ليتضمن TenantID وCreatedAt/CreatedBy وIsDeleted وغيرها.
    /// </summary>
    public class LogEntry : AuditableEntity
    {
        /// <summary>
        /// تاريخ ووقت تنفيذ العملية (UTC). يمكن استخدام CreatedAt أيضاً لكن نحتفظ بحقل صريح للوضوح.
        /// </summary>
        public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// مستوى السجل (Severity).
        /// </summary>
        [Required]
        public LogLevel Level { get; set; }

        /// <summary>
        /// فئة السجل أو الموضوع العام للحدث (مثلاً "Inventory", "Budget", "Auth").
        /// </summary>
        [Required, MaxLength(100)]
        public string Category { get; set; } = default!;

        /// <summary>
        /// نوع العملية المنفذة ضمن الفئة، مثل Create, Update, Delete، أو Execute، أو Custom.
        /// </summary>
        [Required]
        public OperationType Operation { get; set; }

        /// <summary>
        /// اسم الكيان المتأثر بالعملية (مثلاً "Budget", "InventoryItem").
        /// </summary>
        [Required, MaxLength(100)]
        public string EntityName { get; set; } = default!;

        /// <summary>
        /// معرّف الكيان المتأثر كسلسلة نصية، يدعم GUID أو int أو غيرهما.
        /// </summary>
        [Required, MaxLength(50)]
        public string EntityKey { get; set; } = default!;

        /// <summary>
        /// رسالة موجزة تصف الحدث (مثلاً "Created new budget", "Updated quantity on hand").
        /// </summary>
        [Required, MaxLength(200)]
        public string Message { get; set; } = default!;

        /// <summary>
        /// تفاصيل إضافية بصيغة JSON أو نص حر، قد يحتوي حقولًا قبل وبعد التغيير أو بيانات سياقية.
        /// قد يكون null إذا لم تكن هناك تفاصيل. تأكد من حجم معقول في DB (nvarchar(max)).
        /// </summary>
        public string? DetailsJson { get; set; }

        /// <summary>
        /// معرّف الترابط (CorrelationId) لتتبع سلسلة من العمليات المرتبطة.
        /// </summary>
        [MaxLength(100)]
        public string? CorrelationId { get; set; }

        /// <summary>
        /// مصدر الحدث: مثلاً اسم الخدمة أو الخادم أو الجهاز الذي نفذ العملية.
        /// </summary>
        [MaxLength(100)]
        public string? Source { get; set; }

        /// <summary>
        /// علامة إضافية صغيرة أو رمز سياقي. للاستخدام الاختياري.
        /// </summary>
        [MaxLength(200)]
        public string? Tag { get; set; }
 
    }

    /// <summary>
    /// مستويات السجل (Severity).
    /// </summary>
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Critical = 5
    }

    /// <summary>
    /// أنواع العمليات المنفذة.
    /// </summary>
    public enum OperationType
    {
        Create,
        Read,
        Update,
        Delete,
        Execute,
        Authenticate,
        Authorize,
        Custom
    }
}