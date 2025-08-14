using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Commands;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Queries;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.InventoriesController
{
    [Route("api/warehouse")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WarehouseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{warehouseId:guid}/inventories")]
        public async Task<ActionResult<APIResponseDto>> GetInventories(Guid warehouseId)
        {
            var q = new GetInventoriesByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/inventories/count")]
        public async Task<ActionResult<APIResponseDto>> GetTotalInventories(Guid warehouseId)
        {
            var q = new GetTotalInventoriesInWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/products/count")]
        public async Task<ActionResult<APIResponseDto>> GetTotalProducts(Guid warehouseId)
        {
            var q = new GetTotalProductsInWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/count/low-stock")]
        public async Task<ActionResult<APIResponseDto>> GetLowStockCount(Guid warehouseId)
        {
            var q = new GetLowStockCountQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/count/overstock")]
        public async Task<ActionResult<APIResponseDto>> GetOverstockedCount(Guid warehouseId)
        {
            var q = new GetOverstockedCountQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/average-stock")]
        public async Task<ActionResult<APIResponseDto>> GetAverageStockLevel(Guid warehouseId)
        {
            var q = new GetAverageStockLevelQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<APIResponseDto>> GetRecent([FromQuery] int maxCount = 10)
        {
            var q = new GetRecentWarehousesQuery(maxCount);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("inventory-count-per-warehouse")]
        public async Task<ActionResult<APIResponseDto>> GetInventoryCountPerWarehouse()
        {
            var q = new GetInventoryCountPerWarehouseQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetTotalWarehouses()
        {
            var q = new GetWarehousesCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/branch/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByBranch(Guid branchId)
        {
            var q = new GetWarehousesCountByBranchQuery(branchId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid warehouseId)
        {
            var q = new GetWarehouseByIdQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid warehouseId)
        {
            var q = new GetSoftDeletedWarehouseByIdQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllWarehousesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("operational")]
        public async Task<ActionResult<APIResponseDto>> GetOperational()
        {
            var q = new GetOperationalWarehousesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("storage-only")]
        public async Task<ActionResult<APIResponseDto>> GetStorageOnly()
        {
            var q = new GetStorageOnlyWarehousesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-branch/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByBranch(Guid branchId)
        {
            var q = new GetWarehousesByBranchQuery(branchId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetWarehousesByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/with-branch")]
        public async Task<ActionResult<APIResponseDto>> GetWithBranch(Guid warehouseId)
        {
            var q = new GetWarehouseWithBranchQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{warehouseId:guid}/with-inventories")]
        public async Task<ActionResult<APIResponseDto>> GetWithInventories(Guid warehouseId)
        {
            var q = new GetWarehouseWithInventoriesQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateWarehouseDto dto)
        {
            var cmd = new CreateWarehouseCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateWarehouseDto dto)
        {
            var cmd = new UpdateWarehouseCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid warehouseId)
        {
            var dto = new DeleteWarehouseDto { WarehouseId = warehouseId };
            var cmd = new DeleteWarehouseCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] DeleteWarehousesRangeDto dto)
        {
            var cmd = new DeleteWarehousesRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllWarehousesCommand(new DeleteAllWarehousesDto());
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore")]
        public async Task<ActionResult<APIResponseDto>> Restore([FromBody] RestoreWarehouseDto dto)
        {
            var cmd = new RestoreWarehouseCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] RestoreWarehousesRangeDto dto)
        {
            var cmd = new RestoreWarehousesRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAll()
        {
            var cmd = new RestoreAllWarehousesCommand(new RestoreAllWarehousesDto());
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("add-range")]
        public async Task<ActionResult<APIResponseDto>> AddRange([FromBody] AddWarehousesRangeDto dto)
        {
            var cmd = new AddWarehousesRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-import")]
        public async Task<ActionResult<APIResponseDto>> BulkImport([FromBody] BulkImportWarehousesDto dto)
        {
            var cmd = new BulkImportWarehousesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-delete")]
        public async Task<ActionResult<APIResponseDto>> BulkDelete([FromBody] BulkDeleteWarehousesDto dto)
        {
            var cmd = new BulkDeleteWarehousesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("check-exists")]
        public async Task<ActionResult<APIResponseDto>> CheckExists([FromBody] WarehouseExistsDto dto)
        {
            var cmd = new CheckWarehouseExistsCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("check-name")]
        public async Task<ActionResult<APIResponseDto>> CheckName([FromBody] ExistsWarehouseNameDto dto)
        {
            var cmd = new CheckExistsWithNameCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate-branch")]
        public async Task<ActionResult<APIResponseDto>> ValidateBranch([FromBody] ValidateBranchDto dto)
        {
            var cmd = new ValidateBranchCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
