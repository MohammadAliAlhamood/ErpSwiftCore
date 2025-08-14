using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Application.Features.CRMs.Customers.Queries;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Application;
using ErpSwiftCore.Utility;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.CRMsController
{
    [Route("api/customer")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries
        /// <summary>
        /// الحصول على جميع العملاء
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllCustomersQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetCustomerByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetCustomersByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateCustomerDto dto)
        {
            var cmd = new CreateCustomerCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateCustomerDto dto)
        {
            var cmd = new UpdateCustomerCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid CustomerId)
        {
            var cmd = new DeleteCustomerCommand(CustomerId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteCustomersRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreCustomerCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new RestoreCustomersRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion




        #region Queries


        [HttpGet("exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Exists(Guid id)
        {
            var q = new CustomerExistsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/code/{code}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByCode(string code)
        {
            var q = new CustomerExistsByCodeQuery(code);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/email")]
        public async Task<ActionResult<APIResponseDto>> ExistsByEmail([FromQuery] string email)
        {
            var q = new CustomerExistsByEmailQuery(email);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/email/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByEmailExcluding(Guid excludingId, [FromQuery] string email)
        {
            var q = new CustomerExistsByEmailExcludingQuery(email, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/nationalId")]
        public async Task<ActionResult<APIResponseDto>> ExistsByNationalId([FromQuery] string nationalId)
        {
            var q = new CustomerExistsByNationalIdQuery(nationalId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/nationalId/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByNationalIdExcluding(Guid excludingId, [FromQuery] string nationalId)
        {
            var q = new CustomerExistsByNationalIdExcludingQuery(nationalId, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/phone")]
        public async Task<ActionResult<APIResponseDto>> ExistsByPhone([FromQuery] string phone)
        {
            var q = new CustomerExistsByPhoneQuery(phone);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("exists/phone/{excludingId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExistsByPhoneExcluding(Guid excludingId, [FromQuery] string phone)
        {
            var q = new CustomerExistsByPhoneExcludingQuery(phone, excludingId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }




        #endregion
    }
}
