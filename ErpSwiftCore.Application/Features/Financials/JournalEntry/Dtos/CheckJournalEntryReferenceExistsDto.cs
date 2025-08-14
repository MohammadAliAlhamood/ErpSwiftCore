using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos
{
    // ============================
    // DTOs لعمليات التحقق Validation
    // ============================

    /// <summary>
    /// بيانات طلب التحقق من وجود قيد يومية بواسطة رقم المرجع
    /// </summary>
    public class CheckJournalEntryReferenceExistsDto
    {
        public string ReferenceNumber { get; set; } = null!;
    }

    /// <summary>
    /// نتيجة تحقق بوجود قيد اليومية
    /// </summary>
    public class JournalEntryReferenceExistsResultDto
    {
        public bool Exists { get; set; }
    }

    /// <summary>
    /// بيانات طلب التحقق من وجود أي خطوط قيد مرتبطة بحساب
    /// </summary>
    public class CheckJournalEntryLineExistsByAccountDto
    {
        public Guid AccountId { get; set; }
    }

    /// <summary>
    /// نتيجة تحقق بوجود خطوط قيد للحساب
    /// </summary>
    public class JournalEntryLineExistsResultDto
    {
        public bool Exists { get; set; }
    }


    // ============================
    // DTOs لعمليات الـ Command
    // ============================

    /// <summary>
    /// بيانات حذف قيد يومية (Soft or Hard delete)
    /// </summary>
    public class DeleteJournalEntryDto
    {
        public Guid EntryId { get; set; }
    }

    /// <summary>
    /// بيانات استعادة قيد يومية (Restore)
    /// </summary>
    public class RestoreJournalEntryDto
    {
        public Guid EntryId { get; set; }
    }

    /// <summary>
    /// بيانات حذف مجموعة قيود يومية دفعة واحدة
    /// </summary>
    public class BatchDeleteJournalEntriesDto
    {
        public IEnumerable<Guid> EntryIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// بيانات استعادة مجموعة قيود يومية دفعة واحدة
    /// </summary>
    public class BatchRestoreJournalEntriesDto
    {
        public IEnumerable<Guid> EntryIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// بيانات حذف قيد يومية بشكل منطقي (Soft delete)
    /// </summary>
    public class SoftDeleteJournalEntryDto
        : DeleteJournalEntryDto
    {
        // يرث EntryId من DeleteJournalEntryDto
    }

    /// <summary>
    /// بيانات حذف مجموعة قيود يومية منطقياً (Soft batch delete)
    /// </summary>
    public class SoftBatchDeleteJournalEntriesDto
        : BatchDeleteJournalEntriesDto
    {
        // يرث EntryIds من BatchDeleteJournalEntriesDto
    }
}