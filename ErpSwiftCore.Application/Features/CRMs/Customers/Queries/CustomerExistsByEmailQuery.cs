using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    // 3. קיום לפי אימייל (יצירה)
    public class CustomerExistsByEmailQuery : IRequest<APIResponseDto>
    {
        public string Email { get; }
        public CustomerExistsByEmailQuery(string email) => Email = email;
    }
}
