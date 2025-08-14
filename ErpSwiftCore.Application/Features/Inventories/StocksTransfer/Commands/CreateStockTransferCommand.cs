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
    /// إنشاء عملية نقل مخزون جديدة
    /// </summary>
    public class CreateStockTransferCommand : IRequest<APIResponseDto>
    {
        public CreateStockTransferDto Dto { get; }
        public CreateStockTransferCommand(CreateStockTransferDto dto) => Dto = dto;
    }

    /// <summary>
    /// تحديث عملية نقل مخزون موجودة
    /// </summary>
    public class UpdateStockTransferCommand : IRequest<APIResponseDto>
    {
        public UpdateStockTransferDto Dto { get; }
        public UpdateStockTransferCommand(UpdateStockTransferDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف عملية نقل مخزون
    /// </summary>
    public class DeleteStockTransferCommand : IRequest<APIResponseDto>
    {
        public DeleteStockTransferDto Dto { get; }
        public DeleteStockTransferCommand(DeleteStockTransferDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف مجموعة من عمليات نقل المخزون
    /// </summary>
    public class DeleteStockTransfersRangeCommand : IRequest<APIResponseDto>
    {
        public DeleteStockTransfersRangeDto Dto { get; }
        public DeleteStockTransfersRangeCommand(DeleteStockTransfersRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف جميع عمليات نقل المخزون
    /// </summary>
    public class DeleteAllStockTransfersCommand : IRequest<APIResponseDto>
    {
        public DeleteAllStockTransfersCommand() { }
    }

    /// <summary>
    /// استعادة عملية نقل مخزون محذوفة
    /// </summary>
    public class RestoreStockTransferCommand : IRequest<APIResponseDto>
    {
        public RestoreStockTransferDto Dto { get; }
        public RestoreStockTransferCommand(RestoreStockTransferDto dto) => Dto = dto;
    }

    /// <summary>
    /// استعادة مجموعة من عمليات نقل المخزون المحذوفة
    /// </summary>
    public class RestoreStockTransfersRangeCommand : IRequest<APIResponseDto>
    {
        public RestoreStockTransfersRangeDto Dto { get; }
        public RestoreStockTransfersRangeCommand(RestoreStockTransfersRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// استعادة جميع عمليات نقل المخزون المحذوفة
    /// </summary>
    public class RestoreAllStockTransfersCommand : IRequest<APIResponseDto>
    {
        public RestoreAllStockTransfersCommand() { }
    }

    /// <summary>
    /// استيراد دفعي لعمليات نقل المخزون
    /// </summary>
    public class BulkImportStockTransfersCommand : IRequest<APIResponseDto>
    {
        public BulkImportStockTransfersDto Dto { get; }
        public BulkImportStockTransfersCommand(BulkImportStockTransfersDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف دفعي لعمليات نقل المخزون
    /// </summary>
    public class BulkDeleteStockTransfersCommand : IRequest<APIResponseDto>
    {
        public BulkDeleteStockTransfersDto Dto { get; }
        public BulkDeleteStockTransfersCommand(BulkDeleteStockTransfersDto dto) => Dto = dto;
    }
}