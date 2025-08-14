using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Utility;
using ErpSwiftCore.Application.Features.Companies.Currencies.Commands;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Application.Features.Companies.Currencies.Queries;
using ErpSwiftCore.Application;
namespace ErpSwiftCore.API.Controllers.AdminControllers.CompaniesController
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost("create")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> CreateCurrency([FromBody] CurrencyCreateDto dto)
        {
            var command = new CreateCurrencyCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }



        [HttpPut("update")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> UpdateCurrency([FromBody] CurrencyUpdateDto dto)
        {
            var command = new UpdateCurrencyCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }




        [HttpDelete("delete/{currencyId:guid}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> DeleteCurrency(Guid currencyId)
        {
            var command = new DeleteCurrencyCommand(currencyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }



        [HttpGet("{currencyId:guid}")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> GetCurrencyById(Guid currencyId)
        {
            var query = new GetCurrencyByIdQuery(currencyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> GetAllCurrencies()
        {
            var query = new GetAllCurrenciesQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
