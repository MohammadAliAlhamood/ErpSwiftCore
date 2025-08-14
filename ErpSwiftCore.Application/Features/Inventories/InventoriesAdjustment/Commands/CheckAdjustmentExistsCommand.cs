using MediatR; 
using static ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos.UpdateInventoryAdjustmentReasonByDateRangeDto;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Commands
{
    /// <summary>
    /// تحقق من وجود تعديل بمُعرّف معين
    /// </summary>
    public class CheckAdjustmentExistsCommand : IRequest<APIResponseDto>
    {
        public AdjustmentExistsDto Dto { get; }
        public CheckAdjustmentExistsCommand(AdjustmentExistsDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من وجود تعديل لمنتج ومستودع في تاريخ معين مع إمكانية استثناء تعديل
    /// </summary>
    public class CheckExistsForProductWarehouseOnDateCommand : IRequest<APIResponseDto>
    {
        public ExistsForProductWarehouseOnDateDto Dto { get; }
        public CheckExistsForProductWarehouseOnDateCommand(ExistsForProductWarehouseOnDateDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من صلاحية معرّف المنتج
    /// </summary>
    public class ValidateProductCommand : IRequest<APIResponseDto>
    {
        public ValidateProductDto Dto { get; }
        public ValidateProductCommand(ValidateProductDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من صلاحية معرّف المستودع
    /// </summary>
    public class ValidateWarehouseCommand : IRequest<APIResponseDto>
    {
        public ValidateWarehouseDto Dto { get; }
        public ValidateWarehouseCommand(ValidateWarehouseDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من صلاحية الكمية (غير صفرية)
    /// </summary>
    public class ValidateQuantityCommand : IRequest<APIResponseDto>
    {
        public ValidateQuantityDto Dto { get; }
        public ValidateQuantityCommand(ValidateQuantityDto dto) => Dto = dto;
    }
}