using ErpSwiftCore.Application; 
using ErpSwiftCore.Application.Features.Auth.Dtos.PasswordSecurity;
using ErpSwiftCore.Application.Features.Auth.PasswordSecurity.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErpSwiftCore.API.Controllers.AdminControllers.AuthsController
{
    [Route("api/password-security")]
    [ApiController]
    public class PasswordSecurityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PasswordSecurityController(IMediator mediator)
        {
            _mediator = mediator;
        } 
        [HttpPost("password/change")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var command = new ChangePasswordCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// طلب نسيت كلمة المرور (مفتوح لجميع)
        /// </summary>
        [HttpPost("password/forgot")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponseDto>> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            var command = new ForgotPasswordCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// إعادة تعيين كلمة المرور (مفتوح لجميع)
        /// </summary>
        [HttpPost("password/reset")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponseDto>> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            var command = new ResetPasswordCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        } 
    }
}
