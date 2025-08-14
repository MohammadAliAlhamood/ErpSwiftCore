using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.BillingsService
{
    public class InvoiceTaxDiscountService : IInvoiceTaxDiscountService
    {
        private readonly IBaseService _baseService;

        public InvoiceTaxDiscountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetTaxesByInvoiceAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/taxes"
            });

        public async Task<APIResponseDto?> GetTaxesCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/taxes/count"
            });

        public async Task<APIResponseDto?> GetTaxByIdAsync(Guid taxId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/taxes/{taxId}"
            });

        public async Task<APIResponseDto?> GetDiscountsByInvoiceAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discounts"
            });

        public async Task<APIResponseDto?> GetDiscountsCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discounts/count"
            });

        public async Task<APIResponseDto?> GetDiscountByIdAsync(Guid discountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/discounts/{discountId}"
            });

        public async Task<APIResponseDto?> GetTotalTaxAmountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/taxes/total"
            });

        public async Task<APIResponseDto?> GetTotalDiscountAmountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discounts/total"
            });

        public async Task<APIResponseDto?> ValidateTaxAndDiscountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/{invoiceId}"
            });

        public async Task<APIResponseDto?> TaxExistsAsync(Guid taxId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/tax/{taxId}"
            });

        public async Task<APIResponseDto?> DiscountExistsAsync(Guid discountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/discount/{discountId}"
            });

        public async Task<APIResponseDto?> HasTaxesAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/has-taxes/{invoiceId}"
            });

        public async Task<APIResponseDto?> HasDiscountsAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/has-discounts/{invoiceId}"
            });

        public async Task<APIResponseDto?> IsLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/validate/linked-currency/{invoiceId}/{currencyId}"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateTaxesAndDiscountsDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateTaxesAndDiscountsDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/update"
            });

        public async Task<APIResponseDto?> AddTaxAsync(Guid invoiceId, CreateInvoiceTaxDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/tax/add"
            });

        public async Task<APIResponseDto?> AddTaxesAsync(Guid invoiceId, IEnumerable<CreateInvoiceTaxDto> dtos) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dtos,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/taxes/add-multiple"
            });

        public async Task<APIResponseDto?> UpdateTaxAsync(UpdateInvoiceTaxDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/tax/update"
            });

        public async Task<APIResponseDto?> DeleteTaxAsync(Guid taxId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/tax/delete/{taxId}"
            });

        public async Task<APIResponseDto?> DeleteAllTaxesAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/taxes/delete-all"
            });

        public async Task<APIResponseDto?> AddDiscountAsync(Guid invoiceId, CreateInvoiceDiscountDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discount/add"
            });

        public async Task<APIResponseDto?> AddDiscountsAsync(Guid invoiceId, IEnumerable<CreateInvoiceDiscountDto> dtos) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dtos,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discounts/add-multiple"
            });

        public async Task<APIResponseDto?> UpdateDiscountAsync(UpdateInvoiceDiscountDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/discount/update"
            });

        public async Task<APIResponseDto?> DeleteDiscountAsync(Guid discountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/discount/delete/{discountId}"
            });

        public async Task<APIResponseDto?> DeleteAllDiscountsAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice-tax/{invoiceId}/discounts/delete-all"
            });
    }
}
