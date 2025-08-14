using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Restore multiple companies by their IDs
    public class BulkRestoreCompaniesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> CompanyIds { get; }

        public BulkRestoreCompaniesCommand(IEnumerable<Guid> companyIds)
        {
            CompanyIds = companyIds;
        }
    }

}
