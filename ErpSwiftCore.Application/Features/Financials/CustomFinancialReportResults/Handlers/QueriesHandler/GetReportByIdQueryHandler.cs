using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Queries;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Handlers.QueriesHandler
{
    // 1. Get report by ID
    public class GetReportByIdQueryHandler
        : BaseHandler<GetReportByIdQuery>
    {
        private readonly IFinancialReportQueryService _svc;
        private readonly IMapper _mapper;

        public GetReportByIdQueryHandler(
            IFinancialReportQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetReportByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetReportByIdQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetReportByIdAsync(req.ReportId, ct);
            return _mapper.Map<CustomFinancialReportResultDto?>(entity);
        }
    }

    // 2. Get all reports
    public class GetAllReportsQueryHandler
        : BaseHandler<GetAllReportsQuery>
    {
        private readonly IFinancialReportQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllReportsQueryHandler(
            IFinancialReportQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllReportsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllReportsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllReportsAsync(req.IncludeDeleted, ct);
            return list.Select(r => _mapper.Map<CustomFinancialReportResultDto>(r));
        }
    }

    // 3. Get reports by company
    public class GetReportsByCompanyQueryHandler
        : BaseHandler<GetReportsByCompanyQuery>
    {
        private readonly IFinancialReportQueryService _svc;
        private readonly IMapper _mapper;

        public GetReportsByCompanyQueryHandler(
            IFinancialReportQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetReportsByCompanyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetReportsByCompanyQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetReportsByCompanyAsync(req.CompanyId, ct);
            return list.Select(r => _mapper.Map<CustomFinancialReportResultDto>(r));
        }
    }

    // 4. Export to Excel
    public class ExportReportToExcelQueryHandler
        : BaseHandler<ExportReportToExcelQuery>
    {
        private readonly IFinancialReportQueryService _svc;

        public ExportReportToExcelQueryHandler(
            IFinancialReportQueryService svc,
            ILogger<BaseHandler<ExportReportToExcelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ExportReportToExcelQuery req,
            CancellationToken ct)
        {
            var bytes = await _svc.ExportReportToExcelAsync(req.ReportId, ct);
            return bytes; // APIResponseDto will wrap as binary payload
        }
    }

    // 5. Export to CSV
    public class ExportReportToCsvQueryHandler
        : BaseHandler<ExportReportToCsvQuery>
    {
        private readonly IFinancialReportQueryService _svc;

        public ExportReportToCsvQueryHandler(
            IFinancialReportQueryService svc,
            ILogger<BaseHandler<ExportReportToCsvQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ExportReportToCsvQuery req,
            CancellationToken ct)
        {
            var csv = await _svc.ExportReportToCsvAsync(req.ReportId, ct);
            return csv;
        }
    }

    // 6. Get recent reports
    public class GetRecentReportsQueryHandler
        : BaseHandler<GetRecentReportsQuery>
    {
        private readonly IFinancialReportQueryService _svc;
        private readonly IMapper _mapper;

        public GetRecentReportsQueryHandler(
            IFinancialReportQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetRecentReportsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetRecentReportsQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetRecentReportsAsync(req.TopCount, ct);
            return list.Select(r => _mapper.Map<CustomFinancialReportResultDto>(r));
        }
    }

    // 7. Get reports count by company
    public class GetReportsCountByCompanyQueryHandler
        : BaseHandler<GetReportsCountByCompanyQuery>
    {
        private readonly IFinancialReportQueryService _svc;

        public GetReportsCountByCompanyQueryHandler(
            IFinancialReportQueryService svc,
            ILogger<BaseHandler<GetReportsCountByCompanyQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetReportsCountByCompanyQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetReportsCountByCompanyAsync(ct);
            // wrap into DTO
            return new ReportsCountByCompanyDto
            {
                Counts = dict.Select(kv => new CompanyReportCountDto
                {
                    CompanyId = kv.Key,
                    Count = kv.Value
                }).ToList()
            };
        }
    }

    // 8. Get total reports count
    public class GetReportsCountQueryHandler
        : BaseHandler<GetReportsCountQuery>
    {
        private readonly IFinancialReportQueryService _svc;

        public GetReportsCountQueryHandler(
            IFinancialReportQueryService svc,
            ILogger<BaseHandler<GetReportsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetReportsCountQuery req,
            CancellationToken ct)
        {
            return await _svc.GetReportsCountAsync(ct);
        
        }
    }
 
}
