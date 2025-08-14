using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Commands;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Handlers
{
    public class LogoutCommandHandler : BaseHandler<LogoutCommand>
    {
        private readonly IAuthenticationLocService _authService;
        public LogoutCommandHandler(IAuthenticationLocService authService, ILogger<LogoutCommandHandler> logger) : base(logger)
        {
            _authService = authService;
        }
        protected override async Task<object?> HandleRequestAsync(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _authService.LogOutAsync(request.LogoutRequest.UserId);
            return new LogoutResponseDto();
        }
    }
    public class LogoutAllSessionsCommandHandler : BaseHandler<LogoutAllSessionsCommand>
    {
        private readonly IAuthenticationLocService _authService;

        public LogoutAllSessionsCommandHandler(IAuthenticationLocService authService, ILogger<LogoutAllSessionsCommandHandler> logger) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(LogoutAllSessionsCommand request, CancellationToken cancellationToken)
        {
            await _authService.LogOutFromAllSessionsAsync(request.LogoutRequest.UserId);
            return new LogoutAllSessionsResponseDto();
        }
    }
}