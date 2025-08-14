 using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility; 
namespace ErpSwiftCore.Web.Service.BillingsService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IBaseService _baseService;

        public InvoiceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> invoiceIds) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = invoiceIds,
                Url = $"{SD.ErpAPIBase}/api/invoice/by-ids"
            });

        public async Task<APIResponseDto?> GetCountAsync(InvoiceStatus? status = null)
        {
            var url = $"{SD.ErpAPIBase}/api/invoice/count";
            if (status.HasValue) url += $"?status={status}";
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = url
            });
        }

        public async Task<APIResponseDto?> GetLinesAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/lines"
            });

        public async Task<APIResponseDto?> GetLinesCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/lines/count"
            });

        public async Task<APIResponseDto?> GetApprovalsAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/approvals"
            });

        public async Task<APIResponseDto?> GetApprovalsCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/approvals/count"
            });

        public async Task<APIResponseDto?> GetApprovalByIdAsync(Guid approvalId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/approvals/{approvalId}"
            });

        public async Task<APIResponseDto?> GetPaymentsAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/payments"
            });

        public async Task<APIResponseDto?> GetPaymentsCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/payments/count"
            });

        public async Task<APIResponseDto?> GetPaymentByIdAsync(Guid paymentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/payments/{paymentId}"
            });

        public async Task<APIResponseDto?> CheckExistsAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/validate/exists/{invoiceId}"
            });

        public async Task<APIResponseDto?> CheckLineExistsAsync(Guid lineId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/validate/line-exists/{lineId}"
            });

        public async Task<APIResponseDto?> CheckApprovalExistsAsync(Guid approvalId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/validate/approval-exists/{approvalId}"
            });

        public async Task<APIResponseDto?> CheckPaymentExistsAsync(Guid paymentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/validate/payment-exists/{paymentId}"
            });

        public async Task<APIResponseDto?> IsLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/linked-currency/{currencyId}"
            });

        public async Task<APIResponseDto?> CalculateTotalAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/total"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateInvoiceDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateInvoiceDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice/delete/{invoiceId}"
            });

        public async Task<APIResponseDto?> AddLineAsync(Guid invoiceId, CreateInvoiceLineDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/lines/add"
            });

        public async Task<APIResponseDto?> AddLinesAsync(Guid invoiceId, IEnumerable<CreateInvoiceLineDto> dtos) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dtos,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/lines/add-multiple"
            });

        public async Task<APIResponseDto?> UpdateLineAsync(UpdateInvoiceLineDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/lines/update"
            });

        public async Task<APIResponseDto?> DeleteLineAsync(Guid lineId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice/lines/delete/{lineId}"
            });

        public async Task<APIResponseDto?> DeleteAllLinesAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/lines/delete-all"
            });

        public async Task<APIResponseDto?> AddApprovalAsync(Guid invoiceId, CreateInvoiceApprovalDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/approvals/add"
            });

        public async Task<APIResponseDto?> UpdateApprovalAsync(UpdateInvoiceApprovalDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/approvals/update"
            });

        public async Task<APIResponseDto?> DeleteApprovalAsync(Guid approvalId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice/approvals/delete/{approvalId}"
            });

        public async Task<APIResponseDto?> AddPaymentAsync(Guid invoiceId, CreatePaymentDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/{invoiceId}/payments/add"
            });

        public async Task<APIResponseDto?> UpdatePaymentAsync(UpdatePaymentDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/payments/update"
            });

        public async Task<APIResponseDto?> DeletePaymentAsync(Guid paymentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/invoice/payments/delete/{paymentId}"
            });

        public async Task<APIResponseDto?> ChangeStatusAsync(ChangeInvoiceStatusDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/status/change"
            });

        public async Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteInvoicesDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/invoice/delete-range"
            });
    }
}
