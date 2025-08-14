using MediatR;
using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Financials.Accounts.Commands;
using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using ErpSwiftCore.Application.Features.Financials.Accounts.Queries;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.FinancialsController
{
    [Route("api/account")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetAccountByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("soft-deleted/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid id)
        {
            var q = new GetSoftDeletedAccountByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllAccountsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetAccountsByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-transaction-type/{type}")]
        public async Task<ActionResult<APIResponseDto>> GetByTransactionType(TransactionType type)
        {
            var q = new GetAccountsByTransactionTypeQuery(type);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

       

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetAccountsCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/by-type/{type}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByType(TransactionType type)
        {
            var q = new GetAccountsCountByTypeQuery(type);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("balance/by-type/{type}")]
        public async Task<ActionResult<APIResponseDto>> GetTotalBalanceByType(TransactionType type)
        {
            var q = new GetTotalBalanceByTypeQuery(type);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateAccountDto dto)
        {
            var cmd = new CreateAccountCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("add-range")]
        public async Task<ActionResult<APIResponseDto>> AddRange([FromBody] IEnumerable<CreateAccountDto> Accounts)
        {
            var cmd = new AddAccountsRangeCommand(Accounts);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteAccountCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        { 
            var cmd = new DeleteAccountsRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllAccountsCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateAccountDto dto)
        {
            var cmd = new UpdateAccountCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreAccountCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRange([FromBody] IEnumerable<Guid> ids)
        { 
            var cmd = new RestoreAccountsRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAll()
        {
            var cmd = new RestoreAllAccountsCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Validation

        [HttpGet("validate/exists/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> CheckExists(Guid id)
        {
            var cmd = new CheckAccountExistsByIdCommand(  id  );
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate/number")]
        public async Task<ActionResult<APIResponseDto>> CheckExistsByNumber([FromBody] string AccountNumber )
        {
            var cmd = new CheckAccountExistsByNumberCommand(AccountNumber);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

      

        [HttpGet("validate/parent/{parentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateParent(Guid parentId)
        {
            var cmd = new ValidateParentAccountCommand(  parentId );
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
