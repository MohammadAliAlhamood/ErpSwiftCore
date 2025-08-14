using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Application.Features.Billings.Payments.Commands;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Application.Features.Billings.Payments.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
namespace ErpSwiftCore.API.Controllers.CompanyControllers.BillingsController
{
    [Route("api/payment")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetPaymentByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-invoice/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByInvoice(Guid invoiceId)
        {
            var q = new GetPaymentsByInvoiceQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCount(Guid invoiceId)
        {
            var q = new GetPaymentsCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] AddPaymentDto dto)
        {
            var cmd = new AddPaymentCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdatePaymentDto dto)
        {
            var cmd = new UpdatePaymentCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
             var cmd = new DeletePaymentCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Validation

        [HttpGet("validate/exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckExists(Guid id)
        {
            var q = new CheckPaymentExistsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
