using AutoMapper; 
using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Auth.Session.Handlers.QueriesHandler
{
    public class GetActiveSessionsQueryHandler : BaseHandler<GetActiveSessionsQuery>
    {
        private readonly IUserProfileService _authService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public GetActiveSessionsQueryHandler(
            IUserProfileService authService,
            ISessionService sessionService,
            IMapper mapper,
            ILogger<GetActiveSessionsQueryHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetActiveSessionsQuery request, CancellationToken cancellationToken)
        {
            var dto = request.GetActiveSessionsRequest;
            var existing = await _authService.GetUserProfileAsync(dto.UserId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.UserId}' غير موجود.");

            var sessions = await _sessionService.GetActiveSessionsAsync(dto.UserId);
            return _mapper.Map<List<SessionDto>>(sessions);
        }
    }
}
