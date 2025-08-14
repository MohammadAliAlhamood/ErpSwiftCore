using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.CommandsHandler
{
    public class UnblockUserCommandHandler : BaseHandler<UnblockUserCommand>
    {
        private readonly IUserProfileService _authService;

        public UnblockUserCommandHandler(
            IUserProfileService authService,
            ILogger<UnblockUserCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UnblockUserRequest; 

            await _authService.UnblockUserAsync(dto.UserId);
            return new { Message = $"تمّ فك الحظر عن المستخدم '{dto.UserId}'." };
        }
    }
}