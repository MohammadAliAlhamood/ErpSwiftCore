using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.Commands.Authentication;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Handlers
{
    public class LoginCommandHandler : BaseHandler<LoginCommand>
    {
        private readonly IAuthenticationLocService _authService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IAuthenticationLocService authService,
            IMapper mapper,
            ILogger<LoginCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginRequest;
            var loginResult = await _authService.LoginAsync(dto.UserName, dto.Password);

            if (loginResult == null)
                throw new DomainException("بيانات الدخول غير صحيحة.");

            var (userEntity, token) = loginResult.Value;

            return new
            {
                Token = token,
                User = userEntity
            };
        }

    }
}
