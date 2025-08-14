using MediatR; 
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands
{
    /// <summary>1. Import invoices from Excel file</summary>
    public class ImportInvoicesFromExcelCommand : IRequest<APIResponseDto>
    {
        public UploadInvoicesFileDto Dto { get; }
        public ImportInvoicesFromExcelCommand(UploadInvoicesFileDto dto) => Dto = dto;
    } 
}
