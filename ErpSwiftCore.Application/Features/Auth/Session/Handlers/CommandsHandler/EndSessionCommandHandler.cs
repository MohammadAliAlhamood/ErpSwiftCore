using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Auth.Session.Handlers.CommandsHandler
{
    public class EndSessionCommandHandler : BaseHandler<EndSessionCommand>
    {
        private readonly ISessionService _authService;

        public EndSessionCommandHandler(
            ISessionService authService,
            ILogger<EndSessionCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(EndSessionCommand request, CancellationToken cancellationToken)
        {
            await _authService.EndSessionAsync(request.EndSessionRequest.SessionId);
            return new { Message = "تمّ إنهاء الجلسة بنجاح." };
        }
    }
}
