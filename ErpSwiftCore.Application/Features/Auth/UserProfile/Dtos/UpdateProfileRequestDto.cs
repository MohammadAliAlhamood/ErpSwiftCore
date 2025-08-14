using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos
{
    /// <summary>
    /// معلومات مطلوبة لتحديث ملف المستخدم (Profile).
    /// </summary>
    public class UpdateProfileRequestDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Address { get; set; }
    }
}
