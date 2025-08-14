using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.QueriesHandler
{
    // 4. Get invoices report
    public class GetInvoicesReportQueryHandler
        : BaseHandler<GetInvoicesReportQuery>
    {
        private readonly IInvoiceReportingQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoicesReportQueryHandler(
            IInvoiceReportingQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoicesReportQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetInvoicesReportQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetInvoicesReportAsync(
                req.Filter.FromDate, req.Filter.ToDate, req.Filter.Status, ct
            );
            return list.Select(i => _mapper.Map<InvoiceDto>(i));
        }
    }


}
