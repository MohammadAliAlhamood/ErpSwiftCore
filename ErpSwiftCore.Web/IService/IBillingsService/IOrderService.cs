using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Service;
using Microsoft.AspNetCore.Mvc;

namespace ErpSwiftCore.Web.IService.IBillingsService
{
    public interface IOrderService
    {
        #region Queries

        Task<APIResponseDto?> GetByIdAsync(Guid orderId);

        Task<APIResponseDto?> GetAllWithDetailsAsync();
        Task<APIResponseDto?> GetByStatusAsync(OrderStatus status);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> orderIds);

        Task<APIResponseDto?> GetLinesAsync(Guid orderId);

        Task<APIResponseDto?> GetLinesCountAsync(Guid orderId);
        Task<APIResponseDto?> ValidateAsync(Guid orderId);
        Task<APIResponseDto?> ExistsAsync(Guid orderId);
        Task<APIResponseDto?> LineExistsAsync(Guid orderLineId);

        Task<APIResponseDto?> HasLinesAsync(Guid orderId);

        Task<APIResponseDto?> IsLinkedAsync(Guid orderId, Guid partyId);

        Task<APIResponseDto?> CalculateTotalAsync(Guid orderId);

        // ─────────────── Command Methods ───────────────

        Task<APIResponseDto?> CreateAsync(CreateOrderDto dto);

        Task<APIResponseDto?> UpdateAsync(UpdateOrderDto dto);

        Task<APIResponseDto?> DeleteAsync(Guid orderId);

        Task<APIResponseDto?> AddLineAsync(Guid orderId, CreateOrderLineDto dto);

        Task<APIResponseDto?> AddLinesAsync(Guid orderId, IEnumerable<CreateOrderLineDto> dtos);

        Task<APIResponseDto?> UpdateLineAsync(UpdateOrderLineDto dto);

        Task<APIResponseDto?> DeleteLineAsync(Guid orderLineId);

        Task<APIResponseDto?> DeleteAllLinesAsync(Guid orderId);

        Task<APIResponseDto?> ChangeStatusAsync(ChangeOrderStatusDto dto);

        Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> orderIds);



    Task<APIResponseDto?> CreateWithLinesAsync(CreateOrderWithLinesDto dto);
    Task<APIResponseDto?> UpdateWithLinesAsync(UpdateOrderWithLinesDto dto);

        #endregion
    }
}