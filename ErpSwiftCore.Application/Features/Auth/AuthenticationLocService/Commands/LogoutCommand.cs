using MediatR; 
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos;
namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Commands
{
    public class LogoutCommand : IRequest<APIResponseDto>
    {
        public LogoutRequestDto LogoutRequest { get; set; } = default!;
        public LogoutCommand() { }
        public LogoutCommand(LogoutRequestDto dto) => LogoutRequest = dto;
    }

    public class LogoutAllSessionsCommand : IRequest<APIResponseDto>
    {
        public LogoutRequestDto LogoutRequest { get; set; } = default!;
        public LogoutAllSessionsCommand() { }
        public LogoutAllSessionsCommand(LogoutRequestDto dto) => LogoutRequest = dto;
    }
}