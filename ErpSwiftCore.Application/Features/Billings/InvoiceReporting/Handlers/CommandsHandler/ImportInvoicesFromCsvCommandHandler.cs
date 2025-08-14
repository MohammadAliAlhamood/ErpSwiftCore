using AutoMapper;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Handlers.CommandsHandler
{ 
    public class ImportInvoicesFromCsvCommandHandler  : BaseHandler<ImportInvoicesFromCsvCommand>
    {
        private readonly IInvoiceImportExportCommandService _svc;
        private readonly IMapper _mapper; 
        public ImportInvoicesFromCsvCommandHandler(IInvoiceImportExportCommandService svc, IMapper mapper, ILogger<BaseHandler<ImportInvoicesFromCsvCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            ImportInvoicesFromCsvCommand req,
            CancellationToken ct)
        {
            using var stream = req.Dto.File.OpenReadStream();
            var result = await _svc.ImportInvoicesFromCsvAsync(stream, ct);
            return _mapper.Map<ImportResultDto>(result);
        }
    }

}
