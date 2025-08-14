using MediatR; 
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands
{
    public class ImportInvoicesFromJsonCommand : IRequest<APIResponseDto>
    {
        public UploadInvoicesFileDto Dto { get; }
        public ImportInvoicesFromJsonCommand(UploadInvoicesFileDto dto) => Dto = dto;
    } 
}
