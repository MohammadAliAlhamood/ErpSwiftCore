using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.Commands
{
    /// <summary>
    /// تحقق من وجود مستودع بمعرّف معين
    /// </summary>
    public class CheckWarehouseExistsCommand : IRequest<APIResponseDto>
    {
        public WarehouseExistsDto Dto { get; }
        public CheckWarehouseExistsCommand(WarehouseExistsDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من تكرار اسم مستودع في فرع ما
    /// </summary>
    public class CheckExistsWithNameCommand : IRequest<APIResponseDto>
    {
        public ExistsWarehouseNameDto Dto { get; }
        public CheckExistsWithNameCommand(ExistsWarehouseNameDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحقق من صلاحية الفرع (أن يوجد)
    /// </summary>
    public class ValidateBranchCommand : IRequest<APIResponseDto>
    {
        public ValidateBranchDto Dto { get; }
        public ValidateBranchCommand(ValidateBranchDto dto) => Dto = dto;
    }
}