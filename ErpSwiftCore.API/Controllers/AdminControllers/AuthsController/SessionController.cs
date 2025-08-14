using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application;
using ErpSwiftCore.Utility;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Role.Queries;
namespace ErpSwiftCore.API.Controllers.AdminControllers.AuthsController
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// إنهاء الجلسة الجارية (أي مستخدم مصدق)
        /// </summary>
        [HttpPost("end")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> EndSession([FromBody] EndSessionRequestDto dto)
        {
            var command = new EndSessionCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        /// <summary>
        /// جلب إحصائيات استخدام النظام (Admin أو Company أو Senior Manager)
        /// </summary>
        [HttpGet("stats")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> GetSystemUsageStatistics()
        {
            var query = new GetSystemUsageStatisticsQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        /// <summary>
        /// جلب سجلات نشاط المستخدم (Admin أو HR Manager أو Management Employee)
        /// </summary>
        [HttpGet("activity-logs")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> GetUserActivityLogs([FromQuery] GetUserActivityLogsRequestDto dto)
        {
            var query = new GetUserActivityLogsQuery(dto);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        /// <summary>
        /// جلب الجلسات النشطة (Admin أو HR Manager أو Accounting Employee أو المستخدم نفسه)
        /// </summary>
        [HttpGet("active")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<APIResponseDto>> GetActiveSessions([FromQuery] GetActiveSessionsRequestDto dto)
        {
            var query = new GetActiveSessionsQuery(dto);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
