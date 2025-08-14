using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Commands
{
    /// <summary>
    /// التحقق من وجود عملية النقل بمعرّف محدد
    /// </summary>
    public class CheckStockTransferExistsByIdCommand : IRequest<APIResponseDto>
    {
        public Guid TransferId { get; }
        public CheckStockTransferExistsByIdCommand(Guid transferId) => TransferId = transferId;
    }

    /// <summary>
    /// التحقق من وجود تحويل مكرر لنفس المنتج والمستودعات والتاريخ
    /// </summary>
    public class CheckDuplicateTransferCommand : IRequest<APIResponseDto>
    {
        public DuplicateTransferCheckDto Dto { get; }
        public CheckDuplicateTransferCommand(DuplicateTransferCheckDto dto) => Dto = dto;
    }

    /// <summary>
    /// التحقق من صحة المنتج (موجود أم لا)
    /// </summary>
    public class ValidateProductCommand : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public ValidateProductCommand(Guid productId) => ProductId = productId;
    }

    /// <summary>
    /// التحقق من صحة المستودع (موجود أم لا)
    /// </summary>
    public class ValidateWarehouseCommand : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }
        public ValidateWarehouseCommand(Guid warehouseId) => WarehouseId = warehouseId;
    }

    /// <summary>
    /// التحقق من صحة الكمية (أكبر من الصفر)
    /// </summary>
    public class ValidateQuantityCommand : IRequest<APIResponseDto>
    {
        public int Quantity { get; }
        public ValidateQuantityCommand(int quantity) => Quantity = quantity;
    }
}