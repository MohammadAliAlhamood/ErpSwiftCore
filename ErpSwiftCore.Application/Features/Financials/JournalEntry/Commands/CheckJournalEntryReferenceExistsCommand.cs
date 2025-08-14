using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Commands
{
    // ============================
    // Validation Commands
    // ============================

    /// <summary>
    /// تحقق من وجود قيد يومية بواسطة رقم المرجع
    /// </summary>
    public class CheckJournalEntryReferenceExistsCommand : IRequest<APIResponseDto>
    {
        public CheckJournalEntryReferenceExistsDto Dto { get; }
        public CheckJournalEntryReferenceExistsCommand(CheckJournalEntryReferenceExistsDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من وجود أي خطوط قيد مرتبطة بحساب معين
    /// </summary>
    public class CheckJournalEntryLineExistsByAccountCommand : IRequest<APIResponseDto>
    {
        public CheckJournalEntryLineExistsByAccountDto Dto { get; }
        public CheckJournalEntryLineExistsByAccountCommand(CheckJournalEntryLineExistsByAccountDto dto) => Dto = dto;
    }


    // ============================
    // Command Commands
    // ============================

    /// <summary>
    /// حذف قيد يومية (Hard delete)
    /// </summary>
    public class DeleteJournalEntryCommand : IRequest<APIResponseDto>
    {
        public DeleteJournalEntryDto Dto { get; }
        public DeleteJournalEntryCommand(DeleteJournalEntryDto dto) => Dto = dto;
    }

    /// <summary>
    /// استعادة قيد يومية
    /// </summary>
    public class RestoreJournalEntryCommand : IRequest<APIResponseDto>
    {
        public RestoreJournalEntryDto Dto { get; }
        public RestoreJournalEntryCommand(RestoreJournalEntryDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف مجموعة قيود يومية دفعة واحدة
    /// </summary>
    public class BatchDeleteJournalEntriesCommand : IRequest<APIResponseDto>
    {
        public BatchDeleteJournalEntriesDto Dto { get; }
        public BatchDeleteJournalEntriesCommand(BatchDeleteJournalEntriesDto dto) => Dto = dto;
    }

    /// <summary>
    /// استعادة مجموعة قيود يومية دفعة واحدة
    /// </summary>
    public class BatchRestoreJournalEntriesCommand : IRequest<APIResponseDto>
    {
        public BatchRestoreJournalEntriesDto Dto { get; }
        public BatchRestoreJournalEntriesCommand(BatchRestoreJournalEntriesDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف قيد يومية بشكل منطقي (Soft delete)
    /// </summary>
    public class SoftDeleteJournalEntryCommand : IRequest<APIResponseDto>
    {
        public SoftDeleteJournalEntryDto Dto { get; }
        public SoftDeleteJournalEntryCommand(SoftDeleteJournalEntryDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف دفعي لقيد يومية بشكل منطقي (Soft batch delete)
    /// </summary>
    public class SoftBatchDeleteJournalEntriesCommand : IRequest<APIResponseDto>
    {
        public SoftBatchDeleteJournalEntriesDto Dto { get; }
        public SoftBatchDeleteJournalEntriesCommand(SoftBatchDeleteJournalEntriesDto dto) => Dto = dto;
    }
}