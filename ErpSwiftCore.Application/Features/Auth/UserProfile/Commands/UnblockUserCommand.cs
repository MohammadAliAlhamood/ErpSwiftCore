using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Commands
{ 
    public class UnblockUserCommand : IRequest<APIResponseDto>
    {
        public BlockUserRequestDto UnblockUserRequest { get; set; } = default!;

        public UnblockUserCommand() { }

        public UnblockUserCommand(BlockUserRequestDto unblockUserRequest)
        {
            UnblockUserRequest = unblockUserRequest;
        }
    }
}