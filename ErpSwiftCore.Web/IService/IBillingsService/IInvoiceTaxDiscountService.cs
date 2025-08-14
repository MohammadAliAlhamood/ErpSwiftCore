
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.IBillingsService
{
    public interface IInvoiceTaxDiscountService
    {
        // Queries
        Task<APIResponseDto?> GetTaxesByInvoiceAsync(Guid invoiceId);
        Task<APIResponseDto?> GetTaxesCountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetTaxByIdAsync(Guid taxId);
        Task<APIResponseDto?> GetDiscountsByInvoiceAsync(Guid invoiceId);
        Task<APIResponseDto?> GetDiscountsCountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetDiscountByIdAsync(Guid discountId);
        Task<APIResponseDto?> GetTotalTaxAmountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetTotalDiscountAmountAsync(Guid invoiceId);
        Task<APIResponseDto?> ValidateTaxAndDiscountAsync(Guid invoiceId);
        Task<APIResponseDto?> TaxExistsAsync(Guid taxId);
        Task<APIResponseDto?> DiscountExistsAsync(Guid discountId);
        Task<APIResponseDto?> HasTaxesAsync(Guid invoiceId);
        Task<APIResponseDto?> HasDiscountsAsync(Guid invoiceId);
        Task<APIResponseDto?> IsLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateTaxesAndDiscountsDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdateTaxesAndDiscountsDto dto);
        Task<APIResponseDto?> AddTaxAsync(Guid invoiceId, CreateInvoiceTaxDto dto);
        Task<APIResponseDto?> AddTaxesAsync(Guid invoiceId, IEnumerable<CreateInvoiceTaxDto> dtos);
        Task<APIResponseDto?> UpdateTaxAsync(UpdateInvoiceTaxDto dto);
        Task<APIResponseDto?> DeleteTaxAsync(Guid taxId);
        Task<APIResponseDto?> DeleteAllTaxesAsync(Guid invoiceId);
        Task<APIResponseDto?> AddDiscountAsync(Guid invoiceId, CreateInvoiceDiscountDto dto);
        Task<APIResponseDto?> AddDiscountsAsync(Guid invoiceId, IEnumerable<CreateInvoiceDiscountDto> dtos);
        Task<APIResponseDto?> UpdateDiscountAsync(UpdateInvoiceDiscountDto dto);
        Task<APIResponseDto?> DeleteDiscountAsync(Guid discountId);
        Task<APIResponseDto?> DeleteAllDiscountsAsync(Guid invoiceId);
    }
}
