using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    public class DeleteAllInvoiceLinesCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public DeleteAllInvoiceLinesCommand(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
