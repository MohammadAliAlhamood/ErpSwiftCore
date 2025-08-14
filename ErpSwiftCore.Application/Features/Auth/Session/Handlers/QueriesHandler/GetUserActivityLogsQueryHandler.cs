using AutoMapper; 
using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.Session.Handlers.QueriesHandler
{
    public class GetUserActivityLogsQueryHandler : BaseHandler<GetUserActivityLogsQuery>
    {
        private readonly IUserProfileService _authService;
        private readonly IMapper _mapper;
        private readonly IActivityLogsService _activityLogsService;
        public GetUserActivityLogsQueryHandler(
            IUserProfileService authService,
            IActivityLogsService activityLogsService,
            IMapper mapper,
            ILogger<GetUserActivityLogsQueryHandler> logger
        ) : base(logger)
        {
            _activityLogsService = activityLogsService;
            _authService = authService;

            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetUserActivityLogsQuery request, CancellationToken cancellationToken)
        {
            var dto = request.GetUserActivityLogsRequest;
            var existing = await _authService.GetUserProfileAsync(dto.UserId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.UserId}' غير موجود.");

            var logs = await _activityLogsService.GetUserActivityLogsAsync(dto.UserId, dto.StartDate, dto.EndDate);
            return _mapper.Map<List<ActivityLogDto>>(logs);
        }
    }
}
