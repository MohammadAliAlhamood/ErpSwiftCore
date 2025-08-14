using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    // 4. קיום לפי אימייל עם excludingId (עדכון)
    public class CustomerExistsByEmailExcludingQuery : IRequest<APIResponseDto>
    {
        public string Email { get; }
        public Guid ExcludingId { get; }
        public CustomerExistsByEmailExcludingQuery(string email, Guid excludingId)
        {
            Email = email;
            ExcludingId = excludingId;
        }
    }
}
