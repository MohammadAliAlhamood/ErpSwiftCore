using AutoMapper;
using ErpSwiftCore.Application.Features.Auth.Commands.UserProfile;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Handlers
{
    public class RegisterCommandHandler : BaseHandler<RegisterCommand>
    {
        private readonly IAuthenticationLocService _authService;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(
            IAuthenticationLocService authService,
            IMapper mapper,
            ILogger<RegisterCommandHandler> logger
        ) : base(logger)
        {
            _authService = authService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(RegisterCommand request, CancellationToken cancellationToken)
        { 

            ApplicationUser user = new ApplicationUser()
            {
                UserName = request.RegisterRequest.UserName,
                Email = request.RegisterRequest.Email,
                NormalizedEmail = request.RegisterRequest.Email.ToUpper(),
                Name = request.RegisterRequest.Name,
                PhoneNumber = request.RegisterRequest.PhoneNumber,
                ProfilePictureUrl = request.RegisterRequest.ProfilePictureUrl,

                Address = request.RegisterRequest.Address 


            };
            // نفّذ التسجيل
            await _authService.RegisterAsync(user,  request.RegisterRequest.Password,    request.RegisterRequest.RoleName);

            return new
            {
                Message = "تمّ تسجيل المستخدم بنجاح.",
            };
        }
    }
     
     
     
}
