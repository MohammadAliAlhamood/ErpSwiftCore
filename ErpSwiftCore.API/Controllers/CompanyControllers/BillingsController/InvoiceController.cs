using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Billings.Invoices.Commands;
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
using ErpSwiftCore.Application.Features.Billings.Invoices.Queries;
using ErpSwiftCore.Application.Features.Billings.Payments.Commands;
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.BillingsController
{
    [Route("api/invoice")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid invoiceId)
        {
            var q = new GetInvoiceByIdQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> invoiceIds)
        {
            var q = new GetInvoicesByIdsQuery(invoiceIds);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount([FromQuery] InvoiceStatus? status)
        {
            var q = new GetInvoicesCountQuery(status);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{invoiceId:guid}/lines")]
        public async Task<ActionResult<APIResponseDto>> GetLines(Guid invoiceId)
        {
            var q = new GetInvoiceLinesQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{invoiceId:guid}/lines/count")]
        public async Task<ActionResult<APIResponseDto>> GetLinesCount(Guid invoiceId)
        {
            var q = new GetInvoiceLinesCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{invoiceId:guid}/approvals")]
        public async Task<ActionResult<APIResponseDto>> GetApprovals(Guid invoiceId)
        {
            var q = new GetInvoiceApprovalsQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{invoiceId:guid}/approvals/count")]
        public async Task<ActionResult<APIResponseDto>> GetApprovalsCount(Guid invoiceId)
        {
            var q = new GetInvoiceApprovalsCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("approvals/{approvalId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetApprovalById(Guid approvalId)
        {
            var q = new GetInvoiceApprovalByIdQuery(approvalId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{invoiceId:guid}/payments")]
        public async Task<ActionResult<APIResponseDto>> GetPayments(Guid invoiceId)
        {
            var q = new GetPaymentsByInvoiceQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Count payments
        [HttpGet("{invoiceId:guid}/payments/count")]
        public async Task<ActionResult<APIResponseDto>> GetPaymentsCount(Guid invoiceId)
        {
            var q = new GetPaymentsCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Get payment by ID
        [HttpGet("payments/{paymentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetPaymentById(Guid paymentId)
        {
            var q = new GetPaymentByIdQuery(paymentId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Invoice exists
        [HttpGet("validate/exists/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> InvoiceExists(Guid invoiceId)
        {
            var q = new InvoiceExistsQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 13. InvoiceLine exists
        [HttpGet("validate/line-exists/{lineId:guid}")]
        public async Task<ActionResult<APIResponseDto>> LineExists(Guid lineId)
        {
            var q = new InvoiceLineExistsQuery(lineId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 14. InvoiceApproval exists
        [HttpGet("validate/approval-exists/{approvalId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ApprovalExists(Guid approvalId)
        {
            var q = new InvoiceApprovalExistsQuery(approvalId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 15. Payment exists
        [HttpGet("validate/payment-exists/{paymentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> PaymentExists(Guid paymentId)
        {
            var q = new PaymentExistsQuery(paymentId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 16. Is linked to currency
        [HttpGet("{invoiceId:guid}/linked-currency/{currencyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> IsLinkedToCurrency(Guid invoiceId, Guid currencyId)
        {
            var q = new IsInvoiceLinkedToCurrencyQuery(invoiceId, currencyId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 17. Calculate total
        [HttpGet("{invoiceId:guid}/total")]
        public async Task<ActionResult<APIResponseDto>> CalculateTotal(Guid invoiceId)
        {
            var q = new CalculateInvoiceTotalQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands



        // 3. Delete invoice
        [HttpDelete("delete/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid invoiceId)
        {
             var cmd = new DeleteInvoiceCommand(invoiceId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }






        // 7. Delete invoice line
        [HttpDelete("lines/delete/{lineId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteLine(Guid lineId)
        {
            var cmd = new DeleteInvoiceLineCommand(lineId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Delete all lines of invoice
        [HttpDelete("{invoiceId:guid}/lines/delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllLines(Guid invoiceId)
        {
            var cmd = new DeleteAllLinesOfInvoiceCommand(invoiceId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Add invoice approval
        [HttpPost("{invoiceId:guid}/approvals/add")]
        public async Task<ActionResult<APIResponseDto>> AddApproval(Guid invoiceId, [FromBody] CreateInvoiceApprovalDto dto)
        {
            var cmd = new AddInvoiceApprovalCommand(invoiceId, dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Update invoice approval
        [HttpPut("approvals/update")]
        public async Task<ActionResult<APIResponseDto>> UpdateApproval([FromBody] UpdateInvoiceApprovalDto dto)
        {
            var cmd = new UpdateInvoiceApprovalCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Delete invoice approval
        [HttpDelete("approvals/delete/{approvalId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteApproval(Guid approvalId)
        {
             var cmd = new DeleteInvoiceApprovalCommand(approvalId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Add payment
        [HttpPost("{invoiceId:guid}/payments/add")]
        public async Task<ActionResult<APIResponseDto>> AddPayment([FromBody] AddPaymentDto dto)
        {
            var cmd = new AddPaymentCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 13. Update payment
        [HttpPut("payments/update")]
        public async Task<ActionResult<APIResponseDto>> UpdatePayment([FromBody] UpdatePaymentDto dto)
        {
            var cmd = new UpdatePaymentCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 14. Delete payment
        [HttpDelete("payments/delete/{paymentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeletePayment(Guid paymentId)
        {
            var cmd = new DeletePaymentCommand(paymentId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 15. Change invoice status
        [HttpPut("status/change")]
        public async Task<ActionResult<APIResponseDto>> ChangeStatus([FromBody] ChangeInvoiceStatusDto dto)
        {
            var cmd = new ChangeInvoiceStatusCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 16. Bulk delete invoices
        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> BulkDelete([FromBody] IEnumerable<Guid> InvoiceIds)
        {
            var cmd = new BulkDeleteInvoicesCommand(InvoiceIds);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
