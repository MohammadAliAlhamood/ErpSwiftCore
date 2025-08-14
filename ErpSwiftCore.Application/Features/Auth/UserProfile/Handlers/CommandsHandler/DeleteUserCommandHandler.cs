using ErpSwiftCore.Application.Features.Auth.UserProfile.Commands;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Handlers.CommandsHandler
{
    public class DeleteUserCommandHandler : BaseHandler<DeleteUserCommand>
    {
        private readonly IUserProfileService _authService;

        public DeleteUserCommandHandler(
            IUserProfileService authService,
            ILogger<DeleteUserCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DeleteUserRequest;
            var existing = await _authService.GetUserProfileAsync(dto.UserId);
            if (existing == null)
                throw new DomainNotFoundException($"المستخدم بالمعرّف '{dto.UserId}' غير موجود.");

            await _authService.DeleteUserAsync(dto.UserId);
            return new { Message = $"تمّ حذف المستخدم '{dto.UserId}' نهائيًا." };
        }
    }
}
