using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Commands;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using ErpSwiftCore.Application.Features.Auth.Commands.Authentication;
using ErpSwiftCore.Application.Features.Auth.Commands.UserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.AdminControllers.AuthsController
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponseDto>> Register([FromBody] RegisterRequestDto dto)
        {
            RegisterCommand command = new(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            LoginCommand command = new(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> Logout([FromBody] LogoutRequestDto dto)
        {
            LogoutCommand command = new(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        } 
        [HttpPost("logout-all-sessions")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> LogoutAllSessions([FromBody] LogoutRequestDto dto)
        {
            LogoutAllSessionsCommand command = new(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}