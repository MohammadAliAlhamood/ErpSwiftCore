using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Commands;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Handlers.CommandsHandler
{

    // 1. Save (create or update) report
    public class SaveReportCommandHandler
        : BaseHandler<SaveReportCommand>
    {
        private readonly IFinancialReportCommandService _svc;
        private readonly IMapper _mapper;

        public SaveReportCommandHandler(
            IFinancialReportCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<SaveReportCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SaveReportCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<CustomFinancialReportResult>(req.Dto);
            var id = await _svc.SaveReportAsync(entity, ct);
            return new { Id = id };
        }
    }

    // 2. Save reports range
    public class SaveReportsRangeCommandHandler
        : BaseHandler<SaveReportsRangeCommand>
    {
        private readonly IFinancialReportCommandService _svc;
        private readonly IMapper _mapper;

        public SaveReportsRangeCommandHandler(
            IFinancialReportCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<SaveReportsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            SaveReportsRangeCommand req,
            CancellationToken ct)
        {
            var entities = req.Dto.Reports
                                  .Select(dto => _mapper.Map<CustomFinancialReportResult>(dto))
                                  .ToList();
            var ids = await _svc.SaveReportsRangeAsync(entities, ct);
            return new { Ids = ids };
        }
    }

    // 3. Delete single report
    public class DeleteReportCommandHandler
        : BaseHandler<DeleteReportCommand>
    {
        private readonly IFinancialReportCommandService _svc;

        public DeleteReportCommandHandler(
            IFinancialReportCommandService svc,
            ILogger<BaseHandler<DeleteReportCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteReportCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteReportAsync(req.ReportId, ct);
            return new { Success = ok };
        }
    }

    // 4. Delete reports range
    public class DeleteReportsRangeCommandHandler
        : BaseHandler<DeleteReportsRangeCommand>
    {
        private readonly IFinancialReportCommandService _svc;

        public DeleteReportsRangeCommandHandler(
            IFinancialReportCommandService svc,
            ILogger<BaseHandler<DeleteReportsRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteReportsRangeCommand req,
            CancellationToken ct)
        {
            var count = await _svc.DeleteReportsRangeAsync(req.ReportIds, ct);
            return new { DeletedCount = count };
        }
    }

    // 5. Restore single report
    public class RestoreReportCommandHandler
        : BaseHandler<RestoreReportCommand>
    {
        private readonly IFinancialReportCommandService _svc;

        public RestoreReportCommandHandler(
            IFinancialReportCommandService svc,
            ILogger<BaseHandler<RestoreReportCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreReportCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreReportAsync(req.ReportId, ct);
            return new { Success = ok };
        }
    }
 

    // 7. Validate report
    public class ValidateReportCommandHandler
        : BaseHandler<ValidateReportCommand>
    {
        private readonly IFinancialReportValidationService _validation;
        private readonly IMapper _mapper;

        public ValidateReportCommandHandler(
            IFinancialReportValidationService validation,
            IMapper mapper,
            ILogger<BaseHandler<ValidateReportCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateReportCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<CustomFinancialReportResult>(req.Dto);
            var isValid = await _validation.ValidateReportAsync(entity, ct);
            return new { IsValid = isValid };
        }
    }
}