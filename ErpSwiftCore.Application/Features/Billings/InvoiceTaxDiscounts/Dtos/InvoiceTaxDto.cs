using ErpSwiftCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Dtos
{
    /// <summary>
    /// تمثيل ضريبة الفاتورة
    /// </summary>
    public class InvoiceTaxDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }

    /// <summary>
    /// تمثيل خصم الفاتورة
    /// </summary>
    public class InvoiceDiscountDto : AuditableEntityDto
    {
        public Guid InvoiceId { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    // ===== Create/Update Combined =====

    /// <summary>
    /// إنشاء مجموعة ضرائب وخصومات لفاتورة
    /// </summary>
    public class CreateTaxesAndDiscountsDto
    {
        public Guid InvoiceId { get; set; }
        public IEnumerable<CreateInvoiceTaxDto> Taxes { get; set; } = new List<CreateInvoiceTaxDto>();
        public IEnumerable<CreateInvoiceDiscountDto> Discounts { get; set; } = new List<CreateInvoiceDiscountDto>();
    }

    /// <summary>
    /// تحديث مجموعة ضرائب وخصومات لفاتورة
    /// </summary>
    public class UpdateTaxesAndDiscountsDto
    {
        public Guid InvoiceId { get; set; }

        public IEnumerable<CreateInvoiceTaxDto>? TaxesToAdd { get; set; }
        public IEnumerable<UpdateInvoiceTaxDto>? TaxesToUpdate { get; set; }
        public IEnumerable<Guid>? TaxIdsToDelete { get; set; }

        public IEnumerable<CreateInvoiceDiscountDto>? DiscountsToAdd { get; set; }
        public IEnumerable<UpdateInvoiceDiscountDto>? DiscountsToUpdate { get; set; }
        public IEnumerable<Guid>? DiscountIdsToDelete { get; set; }
    }

    // ===== Individual Tax Commands =====

    /// <summary>
    /// بيانات إنشاء ضريبة جديدة
    /// </summary>
    public class CreateInvoiceTaxDto
    {
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }

    /// <summary>
    /// بيانات تحديث ضريبة موجودة
    /// </summary>
    public class UpdateInvoiceTaxDto
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }

   

    // ===== Individual Discount Commands =====

    /// <summary>
    /// بيانات إنشاء خصم جديد
    /// </summary>
    public class CreateInvoiceDiscountDto
    {
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    /// <summary>
    /// بيانات تحديث خصم موجود
    /// </summary>
    public class UpdateInvoiceDiscountDto
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }
    public class InvoiceLinkedToCurrencyDto
    {
        public Guid InvoiceId { get; set; }
        public Guid CurrencyId { get; set; }
    }












     




}