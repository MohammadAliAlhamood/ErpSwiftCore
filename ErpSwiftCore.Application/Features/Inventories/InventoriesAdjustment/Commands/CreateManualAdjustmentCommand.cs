using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Commands
{
    /// <summary>
    /// إنشاء تعديل يدوي
    /// </summary>
    public class CreateManualAdjustmentCommand : IRequest<APIResponseDto>
    {
        public CreateInventoryAdjustmentDto Dto { get; }
        public CreateManualAdjustmentCommand(CreateInventoryAdjustmentDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف تعديل واحد
    /// </summary>
    public class DeleteInventoryAdjustmentCommand : IRequest<APIResponseDto>
    {
        public DeleteInventoryAdjustmentDto Dto { get; }
        public DeleteInventoryAdjustmentCommand(DeleteInventoryAdjustmentDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع تعديل واحد
    /// </summary>
    public class RestoreInventoryAdjustmentCommand : IRequest<APIResponseDto>
    {
        public RestoreInventoryAdjustmentDto Dto { get; }
        public RestoreInventoryAdjustmentCommand(RestoreInventoryAdjustmentDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف مجموعة تعديلات
    /// </summary>
    public class DeleteInventoryAdjustmentsRangeCommand : IRequest<APIResponseDto>
    {
        public DeleteInventoryAdjustmentsRangeDto Dto { get; }
        public DeleteInventoryAdjustmentsRangeCommand(DeleteInventoryAdjustmentsRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف جميع التعديلات
    /// </summary>
    public class DeleteAllInventoryAdjustmentsCommand : IRequest<APIResponseDto>
    {
        public DeleteAllInventoryAdjustmentsDto Dto { get; }
        public DeleteAllInventoryAdjustmentsCommand(DeleteAllInventoryAdjustmentsDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع مجموعة تعديلات
    /// </summary>
    public class RestoreInventoryAdjustmentsRangeCommand : IRequest<APIResponseDto>
    {
        public RestoreInventoryAdjustmentsRangeDto Dto { get; }
        public RestoreInventoryAdjustmentsRangeCommand(RestoreInventoryAdjustmentsRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// استرجاع جميع التعديلات المحذوفة نرمياً
    /// </summary>
    public class RestoreAllInventoryAdjustmentsCommand : IRequest<APIResponseDto>
    {
        public RestoreAllInventoryAdjustmentsDto Dto { get; }
        public RestoreAllInventoryAdjustmentsCommand(RestoreAllInventoryAdjustmentsDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف دفعي مع إرجاع عدد المحذوفات
    /// </summary>
    public class BulkDeleteInventoryAdjustmentsCommand : IRequest<APIResponseDto>
    {
        public BulkDeleteInventoryAdjustmentsDto Dto { get; }
        public BulkDeleteInventoryAdjustmentsCommand(BulkDeleteInventoryAdjustmentsDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث مجموعة من التعديلات مع عكس الفروقات على المخزون
    /// </summary>
    public class UpdateInventoryAdjustmentsRangeCommand : IRequest<APIResponseDto>
    {
        public UpdateInventoryAdjustmentsRangeDto Dto { get; }
        public UpdateInventoryAdjustmentsRangeCommand(UpdateInventoryAdjustmentsRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// تغيير سبب التعديل ضمن نطاق زمني
    /// </summary>
    public class UpdateInventoryAdjustmentReasonByDateRangeCommand : IRequest<APIResponseDto>
    {
        public UpdateInventoryAdjustmentReasonByDateRangeDto Dto { get; }
        public UpdateInventoryAdjustmentReasonByDateRangeCommand(UpdateInventoryAdjustmentReasonByDateRangeDto dto) => Dto = dto;
    }
}