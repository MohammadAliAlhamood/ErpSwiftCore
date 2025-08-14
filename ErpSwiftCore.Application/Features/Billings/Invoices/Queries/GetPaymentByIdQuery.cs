using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{ 
    public class GetPaymentByIdQuery : IRequest<APIResponseDto>
    {
        public Guid PaymentId { get; }
        public GetPaymentByIdQuery(Guid paymentId) => PaymentId = paymentId;
    }
}
