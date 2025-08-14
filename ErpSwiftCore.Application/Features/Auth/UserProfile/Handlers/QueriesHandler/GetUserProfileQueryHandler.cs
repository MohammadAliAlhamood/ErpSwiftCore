using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.QueriesHandler
{ 
    public class GetUserProfileQueryHandler : BaseHandler<GetUserProfileQuery>
    {
        private readonly IUserProfileService _authService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public GetUserProfileQueryHandler(
            IUserProfileService authService,
            IRoleService roleService,
            IMapper mapper,
            ILogger<GetUserProfileQueryHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _roleService = roleService;
            
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            ApplicationUser? userEntity = await _authService.GetUserProfileAsync(request.UserId);
            if (userEntity == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{request.UserId}' غير موجود.");
            UserDto userDto;
            try
            {
                  userDto = _mapper.Map<UserDto>(userEntity);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

             return userDto;
        }
    }  
}