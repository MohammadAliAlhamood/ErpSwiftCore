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

    // 7. Get tax & discount summary
    public class GetTaxDiscountSummaryQueryHandler
        : BaseHandler<GetTaxDiscountSummaryQuery>
    {
        private readonly IInvoiceReportingQueryService _svc;
        private readonly IMapper _mapper;

        public GetTaxDiscountSummaryQueryHandler(
            IInvoiceReportingQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetTaxDiscountSummaryQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetTaxDiscountSummaryQuery req, CancellationToken ct)
        {
            var list = await _svc.GetTaxDiscountSummaryAsync(req.FromDate, req.ToDate, ct);
            return list.Select(s => _mapper.Map<InvoiceTaxDiscountSummaryDto>(s));
        }
    }

}
