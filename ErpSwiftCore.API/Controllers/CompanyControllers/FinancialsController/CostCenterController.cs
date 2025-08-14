using ErpSwiftCore.Application; 
using ErpSwiftCore.Application.Features.Financials.Accounts.Commands; 
using ErpSwiftCore.Application.Features.Financials.CostCenters.Commands;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Dtos;
using ErpSwiftCore.Application.Features.Financials.CostCenters.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace ErpSwiftCore.API.Controllers.CompanyControllers.FinancialsController
{
    [Route("api/cost-center")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class CostCenterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CostCenterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetCostCenterByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllCostCentersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<APIResponseDto>> GetByName(string name)
        {
            var q = new GetCostCentersByNameQuery(name);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetCostCentersByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

       

        [HttpGet("with-children/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithChildren(Guid id)
        {
            var q = new GetCostCenterWithChildrenQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetCostCentersCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

       


        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateCostCenterDto dto)
        {
            var cmd = new CreateCostCenterCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("add-range")]
        public async Task<ActionResult<APIResponseDto>> AddRange([FromBody] IEnumerable<CreateCostCenterDto> Centers)
        {
            var cmd = new AddCostCentersRangeCommand(Centers);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("bulk-import")]
        public async Task<ActionResult<APIResponseDto>> BulkImport([FromBody] IEnumerable<CreateCostCenterDto> Centers)
        {
            var cmd = new BulkImportCostCentersCommand(Centers);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateCostCenterDto dto)
        {
            var cmd = new UpdateCostCenterCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        { 
            var cmd = new DeleteCostCenterCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        { 
            var cmd = new BatchDeleteCostCentersCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllCostCentersCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("soft-delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> SoftDelete(Guid id)
        { 
            var cmd = new SoftDeleteCostCenterCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("soft-delete-range")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteRange([FromBody] IEnumerable<Guid> ids)
        { 
            var cmd = new SoftBatchDeleteCostCentersCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("soft-delete-all")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteAll()
        {
            var cmd = new SoftDeleteAllCostCentersCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        { 
            var cmd = new RestoreCostCenterCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        { 
            var cmd = new BatchRestoreCostCentersCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAll()
        {
            var cmd = new RestoreAllCostCentersCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Validation

        [HttpGet("validate/exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckExists(Guid id)
        { 
            var cmd = new CheckCostCenterExistsByIdCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate/code")]
        public async Task<ActionResult<APIResponseDto>> CheckExistsByCode([FromBody] string Code)
        {
            var cmd = new CheckCostCenterExistsByCodeCommand(Code);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate/name")]
        public async Task<ActionResult<APIResponseDto>> CheckExistsByName([FromBody] string Name)
        {
            var cmd = new CheckCostCenterExistsByNameCommand(Name);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

      

        #endregion
    }
}
