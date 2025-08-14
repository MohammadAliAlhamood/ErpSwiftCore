using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Queries; 
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.InventoriesController
{
    [Route("api/inventory-transaction")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        // 1. Get single transaction by its ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetTransactionByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Get first transaction for a given inventory
        [HttpGet("first/{inventoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetFirstForInventory(Guid inventoryId)
        {
            var q = new GetFirstTransactionForInventoryQuery(inventoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Get last transaction for a given inventory
        [HttpGet("last/{inventoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLastForInventory(Guid inventoryId)
        {
            var q = new GetLastTransactionForInventoryQuery(inventoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 4. Get all transactions for a given inventory
        [HttpGet("inventory/{inventoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByInventory(Guid inventoryId)
        {
            var q = new GetTransactionsByInventoryIdQuery(inventoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Get all transactions for a given product
        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetTransactionsByProductIdQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Get all transactions for a given warehouse
        [HttpGet("warehouse/{warehouseId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByWarehouse(Guid warehouseId)
        {
            var q = new GetTransactionsByWarehouseIdQuery(warehouseId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Get all transactions of a specific type
        [HttpGet("type/{transactionType}")]
        public async Task<ActionResult<APIResponseDto>> GetByType(InventoryTransactionType transactionType)
        {
            var q = new GetTransactionsByTypeQuery(transactionType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Get all transactions in a date range
        [HttpGet("date-range")]
        public async Task<ActionResult<APIResponseDto>> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new GetTransactionsByDateRangeQuery(from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Get all transactions affecting the available balance for an inventory
        [HttpGet("affecting-balance/{inventoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetAffectingBalance(Guid inventoryId)
        {
            var q = new GetTransactionsAffectingBalanceQuery(inventoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Search transactions whose notes contain a given term
        [HttpGet("search")]
        public async Task<ActionResult<APIResponseDto>> SearchByNotes([FromQuery] string noteTerm)
        {
            var q = new SearchTransactionsByNotesQuery(noteTerm);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Get total count of all transactions
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetTransactionsCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Sum quantity moved for a product in a date range
        [HttpGet("sum-quantity/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> SumQuantityByProductAndDateRange(
            Guid productId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new SumQuantityByProductAndDateRangeQuery(productId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 13. Calculate turnover rate for a product in a date range
        [HttpGet("turnover-rate/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTurnoverRate(
            Guid productId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new GetTurnoverRateQuery(productId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
