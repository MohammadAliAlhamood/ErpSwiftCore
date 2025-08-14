using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Queries
{

    /// <summary>
    /// استعلام جلب جميع المستخدمين.
    /// لا يحتاج إلى مدخلات.
    /// </summary>
    public class GetAllUsersQuery : IRequest<APIResponseDto> { }
}
