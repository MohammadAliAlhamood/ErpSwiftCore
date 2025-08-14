using ErpSwiftCore.Application; 
using ErpSwiftCore.Application.Features.Auth.Role.Commands;
using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using ErpSwiftCore.Application.Features.Auth.Role.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpSwiftCore.API.Controllers.AdminControllers.AuthsController
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        } 
        [HttpPost("assign-by-email")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> AssignRoleByEmail([FromBody] AssignRoleByEmailRequestDto dto)
        {
            var command = new AssignRoleByEmailCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        } 
        [HttpPost("assign")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> AssignRole([FromBody] AssignRoleRequestDto dto)
        {
            var command = new AssignRoleCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }  
        [HttpPut("permissions/update")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> UpdateRolePermissions([FromBody] UpdateRolePermissionsRequestDto dto)
        {
            var command = new UpdateRolePermissionsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }  
        [HttpGet("roles")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager)]
        public async Task<ActionResult<APIResponseDto>> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        } 
    }
}
