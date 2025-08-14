using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Commands
{

    /// <summary>
    /// تفعيل إعادة الطلب التلقائي مع تحديد الكمية
    /// </summary>
    public class EnableAutoReorderCommand : IRequest<APIResponseDto>
    {
        public EnableAutoReorderDto Dto { get; }
        public EnableAutoReorderCommand(EnableAutoReorderDto dto) => Dto = dto;
    }

    /// <summary>
    /// تعطيل إعادة الطلب التلقائي
    /// </summary>
    public class DisableAutoReorderCommand : IRequest<APIResponseDto>
    {
        public DisableAutoReorderDto Dto { get; }
        public DisableAutoReorderCommand(DisableAutoReorderDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث كامل لسياسة المخزون
    /// </summary>
    public class UpdatePolicyCommand : IRequest<APIResponseDto>
    {
        public UpdatePolicyDto Dto { get; }
        public UpdatePolicyCommand(UpdatePolicyDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث حد إعادة الطلب فقط
    /// </summary>
    public class UpdateReorderLevelCommand : IRequest<APIResponseDto>
    {
        public UpdateReorderLevelDto Dto { get; }
        public UpdateReorderLevelCommand(UpdateReorderLevelDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث الحد الأقصى للمخزون فقط
    /// </summary>
    public class UpdateMaxStockLevelCommand : IRequest<APIResponseDto>
    {
        public UpdateMaxStockLevelDto Dto { get; }
        public UpdateMaxStockLevelCommand(UpdateMaxStockLevelDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث عدة سياسات دفعة واحدة
    /// </summary>
    public class UpdatePoliciesRangeCommand : IRequest<APIResponseDto>
    {
        public UpdatePoliciesRangeDto Dto { get; }
        public UpdatePoliciesRangeCommand(UpdatePoliciesRangeDto dto) => Dto = dto;
    }
}