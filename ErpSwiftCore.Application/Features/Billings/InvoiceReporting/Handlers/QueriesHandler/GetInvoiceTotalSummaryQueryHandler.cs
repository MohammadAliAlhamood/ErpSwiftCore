using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{
    // 5. Get total summary
    public class GetInvoiceTotalSummaryQueryHandler
        : BaseHandler<GetInvoiceTotalSummaryQuery>
    {
        private readonly IInvoiceReportingQueryService _svc;

        public GetInvoiceTotalSummaryQueryHandler(
            IInvoiceReportingQueryService svc,
            ILogger<BaseHandler<GetInvoiceTotalSummaryQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoiceTotalSummaryQuery req,
            CancellationToken ct)
        {
            var dict = await _svc.GetInvoiceTotalSummaryAsync(
                req.Grouping, req.FromDate, req.ToDate, ct
            );
            return dict.Select(kv => new DateTotalDto { Date = kv.Key, Total = kv.Value });
        }
    }

}
