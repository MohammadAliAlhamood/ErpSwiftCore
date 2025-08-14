using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Restore (un‐archive) a single company by its ID
    public class RestoreCompanyCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public RestoreCompanyCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }


}
