using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class InvoiceLineExistsQuery : IRequest<APIResponseDto>
    {
        public Guid LineId { get; }
        public InvoiceLineExistsQuery(Guid lineId) => LineId = lineId;
    }
}
