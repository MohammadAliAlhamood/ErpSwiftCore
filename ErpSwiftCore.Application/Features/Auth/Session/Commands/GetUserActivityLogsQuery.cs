using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
using MediatR;
namespace ErpSwiftCore.Application.Features.Auth.Session.Commands
{
    public class GetUserActivityLogsQuery : IRequest<APIResponseDto>
    { 
        public GetUserActivityLogsRequestDto GetUserActivityLogsRequest { get; set; } = default!;
        public GetUserActivityLogsQuery() { }

        public GetUserActivityLogsQuery(GetUserActivityLogsRequestDto getUserActivityLogsRequest)
        {
            GetUserActivityLogsRequest = getUserActivityLogsRequest;
        }
    }

}
