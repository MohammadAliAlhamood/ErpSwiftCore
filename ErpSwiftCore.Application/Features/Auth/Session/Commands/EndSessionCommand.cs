using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.Session.Commands
{
    /// <summary>
    /// أمر إنهاء جلسة معينة.
    /// يتضمّن EndSessionRequestDto (SessionId).
    /// </summary>
    public class EndSessionCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات إنهاء الجلسة.
        /// </summary>
        public EndSessionRequestDto EndSessionRequest { get; set; } = default!;

        public EndSessionCommand() { }

        public EndSessionCommand(EndSessionRequestDto endSessionRequest)
        {
            EndSessionRequest = endSessionRequest;
        }
    }
}
