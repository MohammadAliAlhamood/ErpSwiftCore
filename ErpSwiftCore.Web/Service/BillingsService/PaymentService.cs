using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels;
namespace ErpSwiftCore.Web.Service.BillingsService
{
    public class PaymentService : IPaymentService
    {
        private readonly IBaseService _baseService;
        public PaymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        // ─────────────── Query Methods ───────────────
        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/payment/{id}"
            });
        public async Task<APIResponseDto?> GetByInvoiceAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/payment/by-invoice/{invoiceId}"
            });
        public async Task<APIResponseDto?> GetCountAsync(Guid invoiceId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/payment/count/{invoiceId}"
            });
        // ─────────────── Command Methods ───────────────
        public async Task<APIResponseDto?> CreateAsync(AddPaymentDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/payment/create"
            });
        public async Task<APIResponseDto?> UpdateAsync(UpdatePaymentDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/payment/update"
            });
        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/payment/delete/{id}"
            });
        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/payment/validate/exists/{id}"
            });
    }
}
