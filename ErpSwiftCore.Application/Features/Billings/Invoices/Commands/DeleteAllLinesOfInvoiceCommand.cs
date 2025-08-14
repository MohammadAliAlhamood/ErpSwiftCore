using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{ 
    public class DeleteAllLinesOfInvoiceCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public DeleteAllLinesOfInvoiceCommand(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
