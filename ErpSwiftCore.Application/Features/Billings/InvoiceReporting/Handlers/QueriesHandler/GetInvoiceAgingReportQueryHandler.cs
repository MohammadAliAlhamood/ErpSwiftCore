using AutoMapper;
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
    // 6. Get aging report
    public class GetInvoiceAgingReportQueryHandler
        : BaseHandler<GetInvoiceAgingReportQuery>
    {
        private readonly IInvoiceReportingQueryService _svc;
        private readonly IMapper _mapper;

        public GetInvoiceAgingReportQueryHandler(
            IInvoiceReportingQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetInvoiceAgingReportQuery>> logger  ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        } 
        protected override async Task<object?> HandleRequestAsync(  GetInvoiceAgingReportQuery req,    CancellationToken ct)
        {
            var report = await _svc.GetInvoiceAgingReportAsync(req.AsOfDate, ct);
            return _mapper.Map<InvoiceAgingReportDto>(report);
        }
    }

}
