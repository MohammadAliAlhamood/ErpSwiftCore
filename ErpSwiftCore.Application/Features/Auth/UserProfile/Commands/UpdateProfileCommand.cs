using ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Commands
{

    /// <summary>
    /// أمر تحديث ملف المستخدم الشخصي.
    /// يتضمّن UpdateProfileRequestDto.
    /// </summary>
    public class UpdateProfileCommand : IRequest<APIResponseDto>
    {
        /// <summary>
        /// بيانات تحديث الملف الشخصي (Name, PhoneNumber, ProfilePictureUrl, Address).
        /// </summary>
        public UpdateProfileRequestDto UpdateProfileRequest { get; set; } = default!;

        public UpdateProfileCommand() { }

        public UpdateProfileCommand(UpdateProfileRequestDto updateProfileRequest)
        {
            UpdateProfileRequest = updateProfileRequest;
        }
    }
}
