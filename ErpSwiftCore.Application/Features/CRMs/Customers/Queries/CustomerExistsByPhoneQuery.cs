using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    public class CustomerExistsByPhoneQuery : IRequest<APIResponseDto>
    {
        public string Phone { get; }
        public CustomerExistsByPhoneQuery(string phone) => Phone = phone;
    }
}
