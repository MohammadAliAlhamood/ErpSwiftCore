using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Queries
{
    /// <summary>
    /// 1. Get all taxes for an invoice
    /// </summary>
    public class GetTaxesByInvoiceQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetTaxesByInvoiceQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 2. Get taxes count for an invoice
    /// </summary>
    public class GetTaxesCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetTaxesCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 3. Get a single tax by ID
    /// </summary>
    public class GetTaxByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }
        public GetTaxByIdQuery(Guid taxId) => TaxId = taxId;
    }

    /// <summary>
    /// 4. Get all discounts for an invoice
    /// </summary>
    public class GetDiscountsByInvoiceQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetDiscountsByInvoiceQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 5. Get discounts count for an invoice
    /// </summary>
    public class GetDiscountsCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetDiscountsCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 6. Get a single discount by ID
    /// </summary>
    public class GetDiscountByIdQuery : IRequest<APIResponseDto>
    {
        public Guid DiscountId { get; }
        public GetDiscountByIdQuery(Guid discountId) => DiscountId = discountId;
    }

    /// <summary>
    /// 7. Get total tax amount for an invoice
    /// </summary>
    public class GetTotalTaxAmountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetTotalTaxAmountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 8. Get total discount amount for an invoice
    /// </summary>
    public class GetTotalDiscountAmountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetTotalDiscountAmountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    // ===== Validation / Existence =====

    /// <summary>
    /// 9. Validate tax & discount setup for an invoice
    /// </summary>
    public class ValidateTaxAndDiscountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public ValidateTaxAndDiscountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 10. Check if a tax exists
    /// </summary>
    public class TaxExistsQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }
        public TaxExistsQuery(Guid taxId) => TaxId = taxId;
    }

    /// <summary>
    /// 11. Check if a discount exists
    /// </summary>
    public class DiscountExistsQuery : IRequest<APIResponseDto>
    {
        public Guid DiscountId { get; }
        public DiscountExistsQuery(Guid discountId) => DiscountId = discountId;
    }

    /// <summary>
    /// 12. Check if invoice has any taxes
    /// </summary>
    public class HasTaxesQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public HasTaxesQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 13. Check if invoice has any discounts
    /// </summary>
    public class HasDiscountsQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public HasDiscountsQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 14. Check if invoice is linked to a given currency
    /// </summary>
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