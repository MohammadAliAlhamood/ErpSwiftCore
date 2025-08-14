using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Commands;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Queries;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.InventoriesController
{
    [Route("api/inventory-policy")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryPolicyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryPolicyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetPolicyByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-inventory/{inventoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByInventory(Guid inventoryId)
        {
            var q = new GetPolicyByInventoryIdQuery(inventoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllPoliciesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-type/{policyType}")]
        public async Task<ActionResult<APIResponseDto>> GetByType(InventoryPolicyType policyType)
        {
            var q = new GetPoliciesByTypeQuery(policyType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-auto-reorder")]
        public async Task<ActionResult<APIResponseDto>> GetWithAutoReorder()
        {
            var q = new GetPoliciesWithAutoReorderQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("below-reorder")]
        public async Task<ActionResult<APIResponseDto>> GetBelowReorder()
        {
            var q = new GetPoliciesBelowReorderLevelQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("above-max")]
        public async Task<ActionResult<APIResponseDto>> GetAboveMax()
        {
            var q = new GetPoliciesAboveMaxStockLevelQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetPoliciesCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("enable-auto-reorder")]
        public async Task<ActionResult<APIResponseDto>> EnableAutoReorder([FromBody] EnableAutoReorderDto dto)
        {
            var cmd = new EnableAutoReorderCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("disable-auto-reorder")]
        public async Task<ActionResult<APIResponseDto>> DisableAutoReorder([FromBody] DisableAutoReorderDto dto)
        {
            var cmd = new DisableAutoReorderCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> UpdatePolicy([FromBody] UpdatePolicyDto dto)
        {
            var cmd = new UpdatePolicyCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update-reorder-level")]
        public async Task<ActionResult<APIResponseDto>> UpdateReorderLevel([FromBody] UpdateReorderLevelDto dto)
        {
            var cmd = new UpdateReorderLevelCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update-max-stock-level")]
        public async Task<ActionResult<APIResponseDto>> UpdateMaxStockLevel([FromBody] UpdateMaxStockLevelDto dto)
        {
            var cmd = new UpdateMaxStockLevelCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update-range")]
        public async Task<ActionResult<APIResponseDto>> UpdatePoliciesRange([FromBody] UpdatePoliciesRangeDto dto)
        {
            var cmd = new UpdatePoliciesRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
