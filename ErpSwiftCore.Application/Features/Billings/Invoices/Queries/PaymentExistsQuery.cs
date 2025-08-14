using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class PaymentExistsQuery : IRequest<APIResponseDto>
    {
        public Guid PaymentId { get; }
        public PaymentExistsQuery(Guid paymentId) => PaymentId = paymentId;
    }
}
