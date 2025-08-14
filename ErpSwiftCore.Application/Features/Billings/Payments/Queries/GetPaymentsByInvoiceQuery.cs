using ErpSwiftCore.Domain.Enums;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.Payments.Queries
{
    // 1. Get payments by invoice
    public class GetPaymentsByInvoiceQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetPaymentsByInvoiceQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 2. Get approvals by invoice
    public class GetInvoiceApprovalsQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceApprovalsQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 3. Get lines by invoice
    public class GetInvoiceLinesQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceLinesQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 4. Get invoice by ID
    public class GetInvoiceByIdQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceByIdQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 5. Get approval by ID
    public class GetInvoiceApprovalByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ApprovalId { get; }
        public GetInvoiceApprovalByIdQuery(Guid approvalId) => ApprovalId = approvalId;
    }

    // 6. Get payment by ID
    public class GetPaymentByIdQuery : IRequest<APIResponseDto>
    {
        public Guid PaymentId { get; }
        public GetPaymentByIdQuery(Guid paymentId) => PaymentId = paymentId;
    }

    // 7. Get invoices by multiple IDs
    public class GetInvoicesByIdsQuery : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> InvoiceIds { get; }
        public GetInvoicesByIdsQuery(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    }

    // 8. Get payments count for an invoice
    public class GetPaymentsCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetPaymentsCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 9. Get approvals count for an invoice
    public class GetInvoiceApprovalsCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceApprovalsCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 10. Get total invoice count (optional by status)
    public class GetInvoicesCountQuery : IRequest<APIResponseDto>
    {
        public InvoiceStatus? Status { get; }
        public GetInvoicesCountQuery(InvoiceStatus? status = null) => Status = status;
    }

    // 11. Get invoice lines count
    public class GetInvoiceLinesCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetInvoiceLinesCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
    // Validation as queries

    // 12. Validate invoice
    public class ValidateInvoiceQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public ValidateInvoiceQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 13. Check invoice exists
    public class CheckInvoiceExistsQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CheckInvoiceExistsQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 14. Check invoice line exists
    public class CheckInvoiceLineExistsQuery : IRequest<APIResponseDto>
    {
        public Guid LineId { get; }
        public CheckInvoiceLineExistsQuery(Guid lineId) => LineId = lineId;
    }

    // 15. Check invoice approval exists
    public class CheckInvoiceApprovalExistsQuery : IRequest<APIResponseDto>
    {
        public Guid ApprovalId { get; }
        public CheckInvoiceApprovalExistsQuery(Guid approvalId) => ApprovalId = approvalId;
    }

    // 16. Check payment exists
    public class CheckPaymentExistsQuery : IRequest<APIResponseDto>
    {
        public Guid PaymentId { get; }
        public CheckPaymentExistsQuery(Guid paymentId) => PaymentId = paymentId;
    }

    // 17. Check invoice has any lines
    public class CheckHasInvoiceLinesQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CheckHasInvoiceLinesQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // 18. Check invoice linked to currency
    public class CheckInvoiceLinkedToCurrencyQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public Guid CurrencyId { get; }
        public CheckInvoiceLinkedToCurrencyQuery(Guid invoiceId, Guid currencyId)
        {
            InvoiceId = invoiceId;
            CurrencyId = currencyId;
        }
    }

    // 19. Calculate invoice total
    public class CalculateInvoiceTotalQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CalculateInvoiceTotalQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}