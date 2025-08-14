using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Commands;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos.UpdateInventoryAdjustmentReasonByDateRangeDto;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.InventoriesController
{
    [Route("api/adjustment")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class AdjustmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdjustmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetAdjustmentByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllAdjustmentsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/all")]
        public async Task<ActionResult<APIResponseDto>> GetAllSoftDeleted()
        {
            var q = new GetAllSoftDeletedAdjustmentsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid id)
        {
            var q = new GetSoftDeletedAdjustmentByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetAdjustmentsByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId, [FromQuery] Guid? warehouseId = null)
        {
            var q = new GetAdjustmentsByProductQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByWarehouse(Guid warehouseId)
        {
            var q = new GetAdjustmentsByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-date")]
        public async Task<ActionResult<APIResponseDto>> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] Guid? warehouseId = null)
        {
            var q = new GetAdjustmentsByDateRangeQuery(from, to, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-stocktake/{stockTakeId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByStockTake(Guid stockTakeId)
        {
            var q = new GetAdjustmentsByStockTakeIdQuery(stockTakeId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("counts/by-reason")]
        public async Task<ActionResult<APIResponseDto>> GetCountsByReason(
            [FromQuery] Guid? warehouseId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var q = new GetAdjustmentCountsByReasonQuery(warehouseId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetAdjustmentsCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetAdjustmentsCountByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByWarehouse(Guid warehouseId)
        {
            var q = new GetAdjustmentsCountByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-product/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid id)
        {
            var q = new GetAdjustmentWithProductQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-warehouse/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithWarehouse(Guid id)
        {
            var q = new GetAdjustmentWithWarehouseQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("last/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLastAdjustment(Guid productId, Guid warehouseId)
        {
            var q = new GetLastAdjustmentQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("current-stock/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCurrentStock(Guid productId, Guid warehouseId)
        {
            var q = new GetCurrentStockAfterAdjustmentsQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("sum-quantity/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> SumQuantityByDateRange(
            Guid productId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new SumQuantityChangeByProductAndDateRangeQuery(productId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateInventoryAdjustmentDto dto)
        {
            var cmd = new CreateManualAdjustmentCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteInventoryAdjustmentCommand(new DeleteInventoryAdjustmentDto { AdjustmentId = id });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreInventoryAdjustmentCommand(new RestoreInventoryAdjustmentDto { AdjustmentId = id });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new DeleteInventoryAdjustmentsRangeDto { AdjustmentIds = ids };
            var cmd = new DeleteInventoryAdjustmentsRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllInventoryAdjustmentsCommand(new DeleteAllInventoryAdjustmentsDto());
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new RestoreInventoryAdjustmentsRangeDto { AdjustmentIds = ids };
            var cmd = new RestoreInventoryAdjustmentsRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAll()
        {
            var cmd = new RestoreAllInventoryAdjustmentsCommand(new RestoreAllInventoryAdjustmentsDto());
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-delete")]
        public async Task<ActionResult<APIResponseDto>> BulkDelete([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new BulkDeleteInventoryAdjustmentsDto { AdjustmentIds = ids };
            var cmd = new BulkDeleteInventoryAdjustmentsCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update-range")]
        public async Task<ActionResult<APIResponseDto>> UpdateRange([FromBody] UpdateInventoryAdjustmentsRangeDto dto)
        {
            var cmd = new UpdateInventoryAdjustmentsRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update-reason")]
        public async Task<ActionResult<APIResponseDto>> UpdateReason([FromBody] UpdateInventoryAdjustmentReasonByDateRangeDto dto)
        {
            var cmd = new UpdateInventoryAdjustmentReasonByDateRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
        #region Validation Commands

        [HttpGet("validate/exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckExists(Guid id)
        {
            var cmd = new CheckAdjustmentExistsCommand(new AdjustmentExistsDto { AdjustmentId = id });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate/exists-on-date")]
        public async Task<ActionResult<APIResponseDto>> CheckExistsOnDate([FromBody] ExistsForProductWarehouseOnDateDto dto)
        {
            var cmd = new CheckExistsForProductWarehouseOnDateCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateProduct(Guid productId)
        {
            var cmd = new ValidateProductCommand(new ValidateProductDto { ProductId = productId });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateWarehouse(Guid warehouseId)
        {
            var cmd = new ValidateWarehouseCommand(new ValidateWarehouseDto { WarehouseId = warehouseId });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/quantity/{quantity:int}")]
        public async Task<ActionResult<APIResponseDto>> ValidateQuantity(int quantity)
        {
            var cmd = new ValidateQuantityCommand(new ValidateQuantityDto { Quantity = quantity });
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
