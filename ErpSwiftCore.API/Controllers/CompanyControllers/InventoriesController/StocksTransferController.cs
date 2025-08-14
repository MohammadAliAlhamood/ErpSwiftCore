using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Commands;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.InventoriesController
{
    [Route("api/stock-transfer")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class StocksTransferController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StocksTransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetStockTransferByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid id)
        {
            var q = new GetSoftDeletedStockTransferByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllStockTransfersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("active")]
        public async Task<ActionResult<APIResponseDto>> GetActive()
        {
            var q = new GetActiveStockTransfersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/all")]
        public async Task<ActionResult<APIResponseDto>> GetAllSoftDeleted()
        {
            var q = new GetSoftDeletedStockTransfersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetStockTransfersByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetStockTransfersByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-from-warehouse/{fromWarehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByFromWarehouse(Guid fromWarehouseId)
        {
            var q = new GetStockTransfersByFromWarehouseQuery(fromWarehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-to-warehouse/{toWarehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByToWarehouse(Guid toWarehouseId)
        {
            var q = new GetStockTransfersByToWarehouseQuery(toWarehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByWarehouse(Guid warehouseId)
        {
            var q = new GetStockTransfersByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-date-range")]
        public async Task<ActionResult<APIResponseDto>> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new GetStockTransfersByDateRangeQuery(from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("filter")]
        public async Task<ActionResult<APIResponseDto>> GetByFilter([FromBody] string filterExpression)
        {
            // Note: expression must be built client-side or via a known predicate factory
            var q = new GetStockTransfersByFilterQuery(_ => true);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<APIResponseDto>> GetPaged(
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var q = new GetStockTransfersPagedQuery(pageIndex, pageSize);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("paged/active")]
        public async Task<ActionResult<APIResponseDto>> GetPagedByActive(
            [FromQuery] bool isActive,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var q = new GetStockTransfersPagedByActiveStatusQuery(isActive, pageIndex, pageSize);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("paged/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetPagedByProduct(
            Guid productId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var q = new GetStockTransfersPagedByProductQuery(productId, pageIndex, pageSize);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("paged/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetPagedByWarehouse(
            Guid warehouseId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            var q = new GetStockTransfersPagedByWarehouseQuery(warehouseId, pageIndex, pageSize);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-product/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid id)
        {
            var q = new GetStockTransferWithProductQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-warehouses/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithWarehouses(Guid id)
        {
            var q = new GetStockTransferWithWarehousesQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetStockTransfersCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetStockTransfersCountByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByWarehouse(Guid warehouseId)
        {
            var q = new GetStockTransfersCountByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("sum/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTotalQuantityByProduct(
            Guid productId,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var q = new GetTotalTransferredQuantityByProductQuery(productId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("sum/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTotalQuantityByWarehouse(
            Guid warehouseId,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var q = new GetTotalTransferredQuantityByWarehouseQuery(warehouseId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("search/notes")]
        public async Task<ActionResult<APIResponseDto>> SearchByNotes([FromQuery] string term)
        {
            var q = new SearchStockTransfersByNotesQuery(term);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("search/reference")]
        public async Task<ActionResult<APIResponseDto>> SearchByReference([FromQuery] string reference)
        {
            var q = new SearchStockTransfersByReferenceQuery(reference);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("search/product-name")]
        public async Task<ActionResult<APIResponseDto>> SearchByProductName([FromQuery] string productName)
        {
            var q = new SearchStockTransfersByProductNameQuery(productName);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("last/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLastForProduct(Guid productId)
        {
            var q = new GetLastStockTransferForProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("last/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLastForWarehouse(Guid warehouseId)
        {
            var q = new GetLastStockTransferForWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateStockTransferDto dto)
        {
            var cmd = new CreateStockTransferCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateStockTransferDto dto)
        {
            var cmd = new UpdateStockTransferCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteStockTransferCommand(new DeleteStockTransferDto { ID = id });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new DeleteStockTransfersRangeDto { IDs = ids };
            var cmd = new DeleteStockTransfersRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllStockTransfersCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreStockTransferCommand(new RestoreStockTransferDto { ID = id });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new RestoreStockTransfersRangeDto { IDs = ids };
            var cmd = new RestoreStockTransfersRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAll()
        {
            var cmd = new RestoreAllStockTransfersCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-import")]
        public async Task<ActionResult<APIResponseDto>> BulkImport([FromBody] BulkImportStockTransfersDto dto)
        {
            var cmd = new BulkImportStockTransfersCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-delete")]
        public async Task<ActionResult<APIResponseDto>> BulkDelete([FromBody] BulkDeleteStockTransfersDto dto)
        {
            var cmd = new BulkDeleteStockTransfersCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Validation Commands

        [HttpGet("validate/exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckExists(Guid id)
        {
            var cmd = new CheckStockTransferExistsByIdCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate/duplicate")]
        public async Task<ActionResult<APIResponseDto>> CheckDuplicate([FromBody] DuplicateTransferCheckDto dto)
        {
            var cmd = new CheckDuplicateTransferCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateProduct(Guid productId)
        {
            var cmd = new ValidateProductCommand(productId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateWarehouse(Guid warehouseId)
        {
            var cmd = new ValidateWarehouseCommand(warehouseId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/quantity/{quantity:int}")]
        public async Task<ActionResult<APIResponseDto>> ValidateQuantity(int quantity)
        {
            var cmd = new ValidateQuantityCommand(quantity);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
