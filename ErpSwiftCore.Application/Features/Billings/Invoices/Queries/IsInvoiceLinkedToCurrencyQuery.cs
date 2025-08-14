using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class IsInvoiceLinkedToCurrencyQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public Guid CurrencyId { get; }
        public IsInvoiceLinkedToCurrencyQuery(Guid invoiceId, Guid currencyId)
        {
            InvoiceId = invoiceId;
            CurrencyId = currencyId;
        }
    }
}
