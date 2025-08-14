using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Orders.Queries
{
    public class IsOrderLinkedToPartyQuery : IRequest<APIResponseDto>
    {
        public Guid OrderId { get; }
        public Guid PartyId { get; }
        public IsOrderLinkedToPartyQuery(Guid orderId, Guid partyId)
        {
            OrderId = orderId;
            PartyId = partyId;
        }
    }

}
