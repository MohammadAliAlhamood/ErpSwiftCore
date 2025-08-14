using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.Commands
{
    /// <summary>
    /// إنشاء مستودع جديد
    /// </summary>
    public class CreateWarehouseCommand : IRequest<APIResponseDto>
    {
        public CreateWarehouseDto Dto { get; }
        public CreateWarehouseCommand(CreateWarehouseDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث مستودع موجود
    /// </summary>
    public class UpdateWarehouseCommand : IRequest<APIResponseDto>
    {
        public UpdateWarehouseDto Dto { get; }
        public UpdateWarehouseCommand(UpdateWarehouseDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف مستودع واحد
    /// </summary>
    public class DeleteWarehouseCommand : IRequest<APIResponseDto>
    {
        public DeleteWarehouseDto Dto { get; }
        public DeleteWarehouseCommand(DeleteWarehouseDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف مجموعة مستودعات
    /// </summary>
    public class DeleteWarehousesRangeCommand : IRequest<APIResponseDto>
    {
        public DeleteWarehousesRangeDto Dto { get; }
        public DeleteWarehousesRangeCommand(DeleteWarehousesRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف جميع المستودعات
    /// </summary>
    public class DeleteAllWarehousesCommand : IRequest<APIResponseDto>
    {
        public DeleteAllWarehousesDto Dto { get; }
        public DeleteAllWarehousesCommand(DeleteAllWarehousesDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع مستودع واحد
    /// </summary>
    public class RestoreWarehouseCommand : IRequest<APIResponseDto>
    {
        public RestoreWarehouseDto Dto { get; }
        public RestoreWarehouseCommand(RestoreWarehouseDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع مجموعة مستودعات
    /// </summary>
    public class RestoreWarehousesRangeCommand : IRequest<APIResponseDto>
    {
        public RestoreWarehousesRangeDto Dto { get; }
        public RestoreWarehousesRangeCommand(RestoreWarehousesRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع جميع المستودعات
    /// </summary>
    public class RestoreAllWarehousesCommand : IRequest<APIResponseDto>
    {
        public RestoreAllWarehousesDto Dto { get; }
        public RestoreAllWarehousesCommand(RestoreAllWarehousesDto dto) => Dto = dto;
    }

    /// <summary>
    /// إضافة مجموعة مستودعات
    /// </summary>
    public class AddWarehousesRangeCommand : IRequest<APIResponseDto>
    {
        public AddWarehousesRangeDto Dto { get; }
        public AddWarehousesRangeCommand(AddWarehousesRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// استيراد دفعي للمستودعات
    /// </summary>
    public class BulkImportWarehousesCommand : IRequest<APIResponseDto>
    {
        public BulkImportWarehousesDto Dto { get; }
        public BulkImportWarehousesCommand(BulkImportWarehousesDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف دفعي للمستودعات
    /// </summary>
    public class BulkDeleteWarehousesCommand : IRequest<APIResponseDto>
    {
        public BulkDeleteWarehousesDto Dto { get; }
        public BulkDeleteWarehousesCommand(BulkDeleteWarehousesDto dto) => Dto = dto;
    }
} 