using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class CalculateOrderTotalQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public CalculateOrderTotalQuery(Guid orderId) => OrderId = orderId;
    }
}
