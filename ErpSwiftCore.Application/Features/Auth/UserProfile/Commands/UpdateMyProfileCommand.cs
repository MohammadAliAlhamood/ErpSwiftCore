using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using MediatR;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Commands
{
    /// <summary>
    /// أمر لتحديث الملف الشخصي للمستخدم الحالي.
    /// </summary>
    public class UpdateMyProfileCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات تحديث الملف الشخصي للمستخدم الحالي.
        /// </summary>
        public UpdateMyProfileRequestDto UpdateMyProfileRequest { get; set; } = default!;

        public UpdateMyProfileCommand() { }

        public UpdateMyProfileCommand(UpdateMyProfileRequestDto updateMyProfileRequest)
        {
            UpdateMyProfileRequest = updateMyProfileRequest;
        }
    }
}