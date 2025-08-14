using ErpSwiftCore.Application.Features.Financials.JournalEntry.Commands;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.JournalEntry.Handlers.CommandsHandler
{
    // 1. تحقق من وجود قيد يومية بواسطة رقم المرجع
    public class CheckJournalEntryReferenceExistsCommandHandler
        : BaseHandler<CheckJournalEntryReferenceExistsCommand>
    {
        private readonly IJournalEntryValidationService _validation;

        public CheckJournalEntryReferenceExistsCommandHandler(
            IJournalEntryValidationService validation,
            ILogger<BaseHandler<CheckJournalEntryReferenceExistsCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckJournalEntryReferenceExistsCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var exists = await _validation
                .JournalEntryExistsByReferenceAsync(dto.ReferenceNumber!, ct);
            return new JournalEntryReferenceExistsResultDto { Exists = exists };
        }
    }

    // 2. تحقق من وجود أي خطوط قيد مرتبطة بحساب
    public class CheckJournalEntryLineExistsByAccountCommandHandler
        : BaseHandler<CheckJournalEntryLineExistsByAccountCommand>
    {
        private readonly IJournalEntryValidationService _validation;

        public CheckJournalEntryLineExistsByAccountCommandHandler(
            IJournalEntryValidationService validation,
            ILogger<BaseHandler<CheckJournalEntryLineExistsByAccountCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckJournalEntryLineExistsByAccountCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var exists = await _validation
                .JournalEntryLineExistsByAccountAsync(dto.AccountId, ct);
            return new JournalEntryLineExistsResultDto { Exists = exists };
        }
    }


    // ========= Command Handlers =========

    // 3. حذف قيد يومية (Hard delete)
    public class DeleteJournalEntryCommandHandler
        : BaseHandler<DeleteJournalEntryCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public DeleteJournalEntryCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<DeleteJournalEntryCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteJournalEntryCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteJournalEntryAsync(req.Dto.EntryId, ct);
            return new { Success = ok };
        }
    }

    // 4. استعادة قيد يومية
    public class RestoreJournalEntryCommandHandler
        : BaseHandler<RestoreJournalEntryCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public RestoreJournalEntryCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<RestoreJournalEntryCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreJournalEntryCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreJournalEntryAsync(req.Dto.EntryId, ct);
            return new { Success = ok };
        }
    }

    // 5. حذف دفعي لقيود يومية
    public class BatchDeleteJournalEntriesCommandHandler
        : BaseHandler<BatchDeleteJournalEntriesCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public BatchDeleteJournalEntriesCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<BatchDeleteJournalEntriesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BatchDeleteJournalEntriesCommand req,
            CancellationToken ct)
        {
            var count = await _svc.BatchDeleteJournalEntriesAsync(req.Dto.EntryIds, ct);
            return new { DeletedCount = count };
        }
    }

    // 6. استعادة دفعي لقيود يومية
    public class BatchRestoreJournalEntriesCommandHandler
        : BaseHandler<BatchRestoreJournalEntriesCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public BatchRestoreJournalEntriesCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<BatchRestoreJournalEntriesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BatchRestoreJournalEntriesCommand req,
            CancellationToken ct)
        {
            var count = await _svc.BatchRestoreJournalEntriesAsync(req.Dto.EntryIds, ct);
            return new { RestoredCount = count };
        }
    }

    // 7. حذف منطقي لقيد يومية (Soft delete)
    public class SoftDeleteJournalEntryCommandHandler
        : BaseHandler<SoftDeleteJournalEntryCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public SoftDeleteJournalEntryCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<SoftDeleteJournalEntryCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override Task<object?> HandleRequestAsync(
            SoftDeleteJournalEntryCommand req,
            CancellationToken ct)
        {
            return _svc
                .SoftDeleteJournalEntryAsync(req.Dto.EntryId, ct)
                .ContinueWith(t => (object?)new { Success = t.Result }, ct);
        }
    }

    // 8. حذف منطقي دفعي لقيود يومية (Soft batch delete)
    public class SoftBatchDeleteJournalEntriesCommandHandler
        : BaseHandler<SoftBatchDeleteJournalEntriesCommand>
    {
        private readonly IJournalEntryCommandService _svc;

        public SoftBatchDeleteJournalEntriesCommandHandler(
            IJournalEntryCommandService svc,
            ILogger<BaseHandler<SoftBatchDeleteJournalEntriesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            SoftBatchDeleteJournalEntriesCommand req,
            CancellationToken ct)
        {
            var count = await _svc
                .SoftBatchDeleteJournalEntriesAsync(req.Dto.EntryIds, ct);
            return new { DeletedCount = count };
        }
    }
}