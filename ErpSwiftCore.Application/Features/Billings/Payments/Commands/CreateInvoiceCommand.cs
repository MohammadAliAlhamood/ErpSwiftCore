using MediatR; 
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    public class DeleteInvoiceCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId   { get; }
        public DeleteInvoiceCommand(Guid invoiceId) => InvoiceId = invoiceId;
    } 
}