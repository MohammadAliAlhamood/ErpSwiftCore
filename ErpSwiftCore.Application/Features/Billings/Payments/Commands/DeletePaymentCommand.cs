using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    /// <summary>
    /// 14. حذف دفعة
    /// </summary>
    public class DeletePaymentCommand : IRequest<APIResponseDto>
    {
        public Guid PaymentId { get; }
        public DeletePaymentCommand(Guid paymentId) => PaymentId = paymentId;
    }

}
