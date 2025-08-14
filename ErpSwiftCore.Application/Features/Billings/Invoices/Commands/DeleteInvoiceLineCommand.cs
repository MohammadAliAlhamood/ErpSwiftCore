using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    public class DeleteInvoiceLineCommand : IRequest<APIResponseDto>
    {
        public Guid LineId { get; }
        public DeleteInvoiceLineCommand(Guid lineId) => LineId = lineId;
    }
}
