using MediatR;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Queries
{
    public class GetUserProfileQuery :IRequest<APIResponseDto>
    {
        public string UserId { get; set; } = default!;
        public GetUserProfileQuery() { }
        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    } 
}






