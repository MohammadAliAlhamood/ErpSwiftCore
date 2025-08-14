using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.QueriesHandler
{
    public class GetMyProfileQueryHandler : BaseHandler<GetMyProfileQuery>
    {
        private readonly IUserProfileService _authService;
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;

        public GetMyProfileQueryHandler(
            IUserProfileService authService,
            IUserProvider userProvider,
            IMapper mapper,
            ILogger<GetMyProfileQueryHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _userProvider = userProvider;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            // جلب الـ UserId من الـ IUserProvider
            var userId = _userProvider.GetUserId().ToString();

            var existing = await _authService.GetUserProfileAsync(userId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{userId}' غير موجود.");

            // خرائط الـ Entity إلى الـ DTO
            var dto = _mapper.Map<UserDto>(existing);
            return dto;
        }
    }
}