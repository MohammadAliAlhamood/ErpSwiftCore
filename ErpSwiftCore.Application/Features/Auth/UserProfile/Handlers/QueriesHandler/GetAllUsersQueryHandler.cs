using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.QueriesHandler
{
    public class GetAllUsersQueryHandler : BaseHandler<GetAllUsersQuery>
    {
        private readonly IUserProfileService _authService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public GetAllUsersQueryHandler(
            IUserProfileService authService,
            IMapper mapper,
            IRoleService roleService,
        ILogger<GetAllUsersQueryHandler> logger

        ) : base(logger)
        {
            _authService = authService;
            _roleService = roleService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _authService.GetAllUsersAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            // تعبئة الـ Roles لكل مستخدم
            foreach (var dto in userDtos)
            {
                dto.Roles = await _roleService.GetUserRolesAsync(dto.Id);
            }

            return userDtos;
        }
    }


}
