using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{

    public class GetCompaniesByOwnerIdQuery : IRequest<APIResponseDto>
    {
        public Guid OwnerId { get; }

        public GetCompaniesByOwnerIdQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
