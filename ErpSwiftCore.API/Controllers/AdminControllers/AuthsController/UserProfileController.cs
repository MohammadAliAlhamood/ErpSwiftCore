using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.AdminControllers.AuthsController
{
    [Route("api/user-profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{userId}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> GetUserProfile(string userId)
        {
            var query = new GetUserProfileQuery(userId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("my-profile")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> GetMyProfile()
        {
            var query = new GetMyProfileQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        } 
        [HttpGet("users")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager + "," + SD.Role_SeniorManager)]
        public async Task<ActionResult<APIResponseDto>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("update")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> UpdateProfile([FromBody] UpdateProfileRequestDto dto)
        {
            var command = new UpdateProfileCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("update-my-profile")]
        [Authorize]
        public async Task<ActionResult<APIResponseDto>> UpdateMyProfile([FromBody] UpdateMyProfileRequestDto dto)
        {
            var command = new UpdateMyProfileCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("block")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager)]
        public async Task<ActionResult<APIResponseDto>> BlockUser([FromBody] BlockUserRequestDto dto)
        {
            var command = new BlockUserCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> DeleteUser([FromBody] DeleteUserRequestDto dto)
        {
            var command = new DeleteUserCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("unblock")]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager)]
        public async Task<ActionResult<APIResponseDto>> UnblockUser([FromBody] BlockUserRequestDto dto)
        {
            var command = new UnblockUserCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
