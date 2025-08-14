using ErpSwiftCore.Application;

using ErpSwiftCore.Application.Features.Financials.JournalEntry.Commands;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.FinancialsController
{
    [Route("api/journal-entry")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class JournalEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JournalEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id, [FromQuery] bool includeLines = false)
        {
            var q = new GetJournalEntryByIdQuery(id, includeLines);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-account/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByAccount(Guid accountId)
        {
            var q = new GetJournalEntriesByAccountQuery(accountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-date-range")]
        public async Task<ActionResult<APIResponseDto>> GetByDateRange(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new GetJournalEntriesByDateRangeQuery(from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("lines/{journalEntryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetLines(Guid journalEntryId)
        {
            var q = new GetJournalEntryLinesByEntryQuery(journalEntryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("sum/debit/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSumDebit(Guid accountId)
        {
            var q = new GetJournalEntryLineDebitByAccountQuery(accountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("sum/credit/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSumCredit(Guid accountId)
        {
            var q = new GetJournalEntryLineCreditByAccountQuery(accountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("total/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTotal(Guid accountId)
        {
            var q = new GetJournalEntryTotalByAccountQuery(accountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("reconcile/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Reconcile(
            Guid accountId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var q = new ReconcileAccountQuery(accountId, from, to);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Validation Commands

        [HttpGet("validate/reference/{referenceNumber}")]
        public async Task<ActionResult<APIResponseDto>> CheckReferenceExists(string referenceNumber)
        {
            var dto = new CheckJournalEntryReferenceExistsDto { ReferenceNumber = referenceNumber };
            var cmd = new CheckJournalEntryReferenceExistsCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("validate/line-exists/{accountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckLineExistsByAccount(Guid accountId)
        {
            var dto = new CheckJournalEntryLineExistsByAccountDto { AccountId = accountId };
            var cmd = new CheckJournalEntryLineExistsByAccountCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var dto = new DeleteJournalEntryDto { EntryId = id };
            var cmd = new DeleteJournalEntryCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var dto = new RestoreJournalEntryDto { EntryId = id };
            var cmd = new RestoreJournalEntryCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new BatchDeleteJournalEntriesDto { EntryIds = ids };
            var cmd = new BatchDeleteJournalEntriesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new BatchRestoreJournalEntriesDto { EntryIds = ids };
            var cmd = new BatchRestoreJournalEntriesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("soft-delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> SoftDelete(Guid id)
        {
            var dto = new SoftDeleteJournalEntryDto { EntryId = id };
            var cmd = new SoftDeleteJournalEntryCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("soft-delete-range")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var dto = new SoftBatchDeleteJournalEntriesDto { EntryIds = ids };
            var cmd = new SoftBatchDeleteJournalEntriesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
