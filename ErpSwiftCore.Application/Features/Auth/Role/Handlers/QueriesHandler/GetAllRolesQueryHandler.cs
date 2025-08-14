using AutoMapper; 
using ErpSwiftCore.Application.Features.Auth.Role.Dtos;
using ErpSwiftCore.Application.Features.Auth.Role.Queries;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Auth.Role.Handlers.QueriesHandler
{
    public class GetAllRolesQueryHandler : BaseHandler<GetAllRolesQuery>
    {
        private readonly IRoleService _authService;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(
            IRoleService authService,
            IMapper mapper,
            ILogger<GetAllRolesQueryHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllRolesQuery request, 
            CancellationToken cancellationToken)
        {
            var roles = await _authService.GetAllRolesAsync();
            return roles.Select(r => _mapper.Map<RoleDto>(r)).ToList();
        }
    }

}
