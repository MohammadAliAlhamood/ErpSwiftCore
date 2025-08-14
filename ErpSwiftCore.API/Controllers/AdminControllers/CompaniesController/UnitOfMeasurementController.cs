using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Queries; 
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
namespace ErpSwiftCore.API.Controllers.AdminControllers.CompaniesController
{
    [Route("api/unit-of-measurement")]
    [ApiController]
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UnitOfMeasurementController(IMediator mediator)
        {
            _mediator = mediator;
        }
     
        
        [HttpPost("create")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> CreateUnitOfMeasurement([FromBody] UnitOfMeasurementCreateDto dto)
        {
            var command = new CreateUnitOfMeasurementCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
    
        
        
        [HttpPut("update")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> UpdateUnitOfMeasurement([FromBody] UnitOfMeasurementUpdateDto dto)
        {
            var command = new UpdateUnitOfMeasurementCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        
        
        
        [HttpDelete("delete/{unitId:guid}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> DeleteUnitOfMeasurement(Guid unitId)
        {
            var command = new DeleteUnitOfMeasurementCommand(unitId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        
        
        
        
        [HttpDelete("delete-range")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> DeleteUnitsOfMeasurementRange([FromBody] IEnumerable<Guid> unitIds)
        {
            var command = new DeleteUnitsOfMeasurementRangeCommand(unitIds);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        
        
        
        [HttpDelete("delete-all")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> DeleteAllUnitsOfMeasurement()
        {
            var command = new DeleteAllUnitsOfMeasurementCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
     
        
        [HttpGet("{unitId:guid}")]
        [Authorize ]
        public async Task<ActionResult<APIResponseDto>> GetUnitOfMeasurementById(Guid unitId)
        {
            var query = new GetUnitOfMeasurementByIdQuery(unitId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
       
        
        
        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> GetAllUnitsOfMeasurement()
        {
            var query = new GetAllUnitsOfMeasurementQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
