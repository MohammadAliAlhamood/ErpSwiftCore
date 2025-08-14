using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.CommandsHandler
{
    // 3. Import invoices from JSON
    public class ImportInvoicesFromJsonCommandHandler
        : BaseHandler<ImportInvoicesFromJsonCommand>
    {
        private readonly IInvoiceImportExportCommandService _svc;
        private readonly IMapper _mapper;

        public ImportInvoicesFromJsonCommandHandler(
            IInvoiceImportExportCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<ImportInvoicesFromJsonCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            ImportInvoicesFromJsonCommand req,
            CancellationToken ct)
        {
            using var stream = req.Dto.File.OpenReadStream();
            var result = await _svc.ImportInvoicesFromJsonAsync(stream, ct);
            return _mapper.Map<ImportResultDto>(result);
        }
    }

}
