using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    // 1. קיום לפי מזהה
    public class CustomerExistsQuery : IRequest<APIResponseDto>
    {
        public Guid CustomerId { get; }
        public CustomerExistsQuery(Guid customerId) => CustomerId = customerId;
    }
}
