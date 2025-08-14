using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.Auth.Handlers.CommandsHandler
{
    public class UpdateMyProfileCommandHandler : BaseHandler<UpdateMyProfileCommand>
    {
        private readonly IUserProfileService _authService;
        private readonly IUserProvider _userProvider;
        private readonly IMapper _mapper;

        public UpdateMyProfileCommandHandler(
            IUserProfileService authService,
            IUserProvider userProvider,
            IMapper mapper,
            ILogger<UpdateMyProfileCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _userProvider = userProvider;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateMyProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userProvider.GetUserId();
            var dto = request.UpdateMyProfileRequest;

            var existing = await _authService.GetUserProfileAsync(userId.ToString());
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{userId}' غير موجود.");

            _mapper.Map(dto, existing);

            await _authService.UpdateProfileAsync(existing);

            return new { Message = "تمّ تحديث الملف الشخصي بنجاح." };
        }
    }
}
