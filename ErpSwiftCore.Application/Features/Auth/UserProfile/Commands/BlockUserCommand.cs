using MediatR;
using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Commands
{
    public class BlockUserCommand : IRequest<APIResponseDto>
    {
        public BlockUserRequestDto BlockUserRequest { get; set; } = default!;
        public BlockUserCommand() { }
        public BlockUserCommand(BlockUserRequestDto blockUserRequest)
        {
            BlockUserRequest = blockUserRequest;
        }
    }

}
