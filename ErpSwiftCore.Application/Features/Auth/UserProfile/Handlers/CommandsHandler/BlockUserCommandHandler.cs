using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.Handlers.CommandsHandler
{
    public class BlockUserCommandHandler : BaseHandler<BlockUserCommand>
    {
        private readonly IUserProfileService _authService;

        public BlockUserCommandHandler(
            IUserProfileService authService,
            ILogger<BlockUserCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.BlockUserRequest;  
            await _authService.BlockUserAsync(dto.UserId);
            return new { Message = $"تمّ حظر المستخدم '{dto.UserId}'." };
        }
    }

}
