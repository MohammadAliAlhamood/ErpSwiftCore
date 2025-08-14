using MediatR;
using ErpSwiftCore.Application.Features.Auth.Session.Dtos;
namespace ErpSwiftCore.Application.Features.Auth.Session.Commands
{ 
    public class GetActiveSessionsQuery : IRequest<APIResponseDto>
    {
        public GetActiveSessionsRequestDto GetActiveSessionsRequest { get; set; } = default!;
        public GetActiveSessionsQuery() { }
        public GetActiveSessionsQuery(GetActiveSessionsRequestDto getActiveSessionsRequest)
        {
            GetActiveSessionsRequest = getActiveSessionsRequest;
        }
    }
}
