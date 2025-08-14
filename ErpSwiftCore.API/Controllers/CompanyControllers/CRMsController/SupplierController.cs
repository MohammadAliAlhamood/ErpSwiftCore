using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Commands;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.CRMsController
{
    [Route("api/supplier")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries
        [HttpGet]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllSuppliersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetSupplierByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Exists(Guid id)
        {
            var q = new SupplierExistsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/code/{code}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByCode(string code)
        {
            var q = new SupplierExistsByCodeQuery(code);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/email")]
        public async Task<ActionResult<APIResponseDto>> ExistsByEmail([FromQuery] string email)
        {
            var q = new SupplierExistsByEmailQuery(email);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/email/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByEmailExcluding(Guid excludingId, [FromQuery] string email)
        {
            var q = new SupplierExistsByEmailExcludingQuery(email, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/nationalId")]
        public async Task<ActionResult<APIResponseDto>> ExistsByNationalId([FromQuery] string nationalId)
        {
            var q = new SupplierExistsByNationalIdQuery(nationalId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/nationalId/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByNationalIdExcluding(Guid excludingId, [FromQuery] string nationalId)
        {
            var q = new SupplierExistsByNationalIdExcludingQuery(nationalId, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/phone")]
        public async Task<ActionResult<APIResponseDto>> ExistsByPhone([FromQuery] string phone)
        {
            var q = new SupplierExistsByPhoneQuery(phone);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/phone/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByPhoneExcluding(Guid excludingId, [FromQuery] string phone)
        {
            var q = new SupplierExistsByPhoneExcludingQuery(phone, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        #endregion
        #region Commands
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateSupplierDto dto)
        {
            var cmd = new CreateSupplierCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateSupplierDto dto)
        {
            var cmd = new UpdateSupplierCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteSupplierCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteSuppliersRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreSupplierCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new RestoreSuppliersRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        #endregion
    }
}
