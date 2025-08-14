using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using MediatR; 

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Commands
{ 
    public class DeleteUserCommand : IRequest<APIResponseDto>
    { 
        public DeleteUserRequestDto DeleteUserRequest { get; set; } = default!;
        public DeleteUserCommand() { }

        public DeleteUserCommand(DeleteUserRequestDto deleteUserRequest)
        {
            DeleteUserRequest = deleteUserRequest;
        }
    }
}
