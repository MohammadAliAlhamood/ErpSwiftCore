using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Queries; 
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
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
    [Route("api/inventory")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        // 1. Get inventory by its ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetInventoryByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Get soft‑deleted inventory by its ID
        [HttpGet("soft-deleted/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid id)
        {
            var q = new GetSoftDeletedInventoryByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Get inventory for a product in a specific warehouse
        [HttpGet("by-product/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProductAndWarehouse(Guid productId, Guid warehouseId)
        {
            var q = new GetInventoryByProductAndWarehouseQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 4. Get current inventory snapshot
        [HttpGet("snapshot/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSnapshot(Guid productId, Guid warehouseId)
        {
            var q = new GetCurrentInventorySnapshotQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Get last inventory record
        [HttpGet("last-record/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLastRecord(Guid productId, Guid warehouseId)
        {
            var q = new GetLastInventoryRecordQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Get all inventories
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllInventoriesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Get inventories by product
        [HttpGet("by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetInventoriesByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Get inventories by warehouse
        [HttpGet("by-warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByWarehouse(Guid warehouseId)
        {
            var q = new GetInventoriesByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        

        // 11. Get inventory with product data
        [HttpGet("with-product/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid id)
        {
            var q = new GetInventoryWithProductQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Get inventory with warehouse data
        [HttpGet("with-warehouse/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithWarehouse(Guid id)
        {
            var q = new GetInventoryWithWarehouseQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 13. Get inventory with policy
        [HttpGet("with-policy/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithPolicy(Guid id)
        {
            var q = new GetInventoryWithPolicyQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 14. Get inventory with transactions
        [HttpGet("with-transactions/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithTransactions(Guid id)
        {
            var q = new GetInventoryWithTransactionsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 15. Get inventory with notifications
        [HttpGet("with-notifications/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithNotifications(Guid id)
        {
            var q = new GetInventoryWithNotificationsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 16. Get raw transactions for an inventory
        [HttpGet("{id:guid}/transactions")]
        public async Task<ActionResult<APIResponseDto>> GetTransactions(Guid id)
        {
            var q = new GetInventoryTransactionsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 17. Get policy for an inventory
        [HttpGet("{id:guid}/policy")]
        public async Task<ActionResult<APIResponseDto>> GetPolicy(Guid id)
        {
            var q = new GetInventoryPolicyQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 18. Get total inventories count
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetInventoriesCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 19. Get inventories count by product
        [HttpGet("count/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetInventoriesCountByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 20. Get inventories count by warehouse
        [HttpGet("count/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByWarehouse(Guid warehouseId)
        {
            var q = new GetInventoriesCountByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 21. Get low‑stock count in a warehouse
        [HttpGet("count/low-stock/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLowStockCount(Guid warehouseId)
        {
            var q = new GetLowStockCountQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 22. Get overstocked count in a warehouse
        [HttpGet("count/overstock/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetOverstockedCount(Guid warehouseId)
        {
            var q = new GetOverstockedCountQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 23. Get product availability across warehouses
        [HttpGet("availability/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetAvailability(Guid productId)
        {
            var q = new GetProductAvailabilityAcrossWarehousesQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 24. Get stock summary for multiple products
        [HttpPost("summary/by-products")]
        public async Task<ActionResult<APIResponseDto>> GetStockSummary([FromBody] IEnumerable<Guid> productIds)
        {
            var q = new GetStockSummaryByProductQuery(productIds);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 25. Get total available quantity in a warehouse
        [HttpGet("total-available/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTotalAvailable(Guid warehouseId)
        {
            var q = new GetTotalAvailableQuantityByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 26. Get total reserved quantity in a warehouse
        [HttpGet("total-reserved/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTotalReserved(Guid warehouseId)
        {
            var q = new GetTotalReservedQuantityByWarehouseQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 27. Calculate inventory value in a warehouse
        [HttpGet("value/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> CalculateValue(Guid warehouseId)
        {
            var q = new CalculateInventoryValueQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 28. Get average stock level in a warehouse
        [HttpGet("average/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetAverageLevel(Guid warehouseId)
        {
            var q = new GetAverageStockLevelQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 29. Get turnover rate for a product in a date range
        [HttpGet("turnover/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTurnoverRate(
            Guid productId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new GetTurnoverRateQuery(productId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 30. Get current stock after adjustments for a product in a warehouse
        [HttpGet("current/{productId:guid}/warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCurrentAfterAdjustments(Guid productId, Guid warehouseId)
        {
            var q = new GetCurrentStockAfterAdjustmentsQuery(productId, warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 31. Get all inventories below reorder level
        [HttpGet("below-reorder")]
        public async Task<ActionResult<APIResponseDto>> GetBelowReorder()
        {
            var q = new GetBelowReorderLevelQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 32. Get all inventories above max stock level
        [HttpGet("above-max")]
        public async Task<ActionResult<APIResponseDto>> GetAboveMax()
        {
            var q = new GetAboveMaxStockLevelQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
