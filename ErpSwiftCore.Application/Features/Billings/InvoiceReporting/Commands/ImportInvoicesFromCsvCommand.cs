using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands
{
    /// <summary>2. Import invoices from CSV file</summary>
    public class ImportInvoicesFromCsvCommand : IRequest<APIResponseDto>
    {
        public UploadInvoicesFileDto Dto { get; }
        public ImportInvoicesFromCsvCommand(UploadInvoicesFileDto dto) => Dto = dto;
    }

}
