using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Delete multiple companies by their IDs
    public class BulkDeleteCompaniesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CompanyIds { get; }

        public BulkDeleteCompaniesCommand(IEnumerable<Guid> companyIds)
        {
            CompanyIds = companyIds;
        }
    }
}
