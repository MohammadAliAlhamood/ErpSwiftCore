using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    // 2. קיום לפי קוד לקוח
    public class CustomerExistsByCodeQuery : IRequest<APIResponseDto>
    {
        public string CustomerCode { get; }
        public CustomerExistsByCodeQuery(string customerCode) => CustomerCode = customerCode;
    }
}
