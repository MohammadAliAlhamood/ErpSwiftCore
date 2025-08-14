using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.CommandsHandler
{
    public class UpdateProfileCommandHandler : BaseHandler<UpdateProfileCommand>
    {
        private readonly IUserProfileService _authService;
        private readonly IMapper _mapper;
        public UpdateProfileCommandHandler(
            IUserProfileService authService,
            IMapper mapper,
            ILogger<UpdateProfileCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateProfileRequest;
            var existing = await _authService.GetUserProfileAsync(dto.Id);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.Id}' غير موجود.");

            // نستخدم AutoMapper لتعبئة الحقول القابلة للتغيير في الـ ApplicationUser
            _mapper.Map(dto, existing);
            await _authService.UpdateProfileAsync(existing);

            return new { Message = "تمّ تحديث الملف الشخصي بنجاح." };
        }
    }

}
