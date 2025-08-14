using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Service.BillingsService
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> orderIds) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = orderIds,
                Url = $"{SD.ErpAPIBase}/api/order/by-ids"
            });

        public async Task<APIResponseDto?> GetLinesAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/lines"
            });

        public async Task<APIResponseDto?> GetLinesCountAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/lines/count"
            });

        public async Task<APIResponseDto?> ValidateAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/validate/{orderId}"
            });

        public async Task<APIResponseDto?> ExistsAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/validate/exists/{orderId}"
            });

        public async Task<APIResponseDto?> LineExistsAsync(Guid orderLineId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/validate/line-exists/{orderLineId}"
            });

        public async Task<APIResponseDto?> HasLinesAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/validate/has-lines/{orderId}"
            });

        public async Task<APIResponseDto?> IsLinkedAsync(Guid orderId, Guid partyId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/validate/linked-party/{orderId}/{partyId}"
            });

        public async Task<APIResponseDto?> CalculateTotalAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/total"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateOrderDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateOrderDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/order/delete/{orderId}"
            });

        public async Task<APIResponseDto?> AddLineAsync(Guid orderId, CreateOrderLineDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/lines/add"
            });

        public async Task<APIResponseDto?> AddLinesAsync(Guid orderId, IEnumerable<CreateOrderLineDto> dtos) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dtos,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/lines/add-multiple"
            });

        public async Task<APIResponseDto?> UpdateLineAsync(UpdateOrderLineDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/lines/update"
            });

        public async Task<APIResponseDto?> DeleteLineAsync(Guid orderLineId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/order/lines/delete/{orderLineId}"
            });

        public async Task<APIResponseDto?> DeleteAllLinesAsync(Guid orderId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/order/{orderId}/lines/delete-all"
            });

        public async Task<APIResponseDto?> ChangeStatusAsync(ChangeOrderStatusDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/status/change"
            });

        public async Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> orderIds) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = orderIds,
                Url = $"{SD.ErpAPIBase}/api/order/delete-range"
            });

        // ─────────────── Additional Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllWithDetailsAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/all-with-details"
            });

        public async Task<APIResponseDto?> GetByStatusAsync(OrderStatus status) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/order/by-status/{status}"
            });
        // ─────────────── Extended Command Methods ───────────────

        public async Task<APIResponseDto?> CreateWithLinesAsync(CreateOrderWithLinesDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/create-with-lines"
            });

        public async Task<APIResponseDto?> UpdateWithLinesAsync(UpdateOrderWithLinesDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/order/update-with-lines"
            });

    }
}
