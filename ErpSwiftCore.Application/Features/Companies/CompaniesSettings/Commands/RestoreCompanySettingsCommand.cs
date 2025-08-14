using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Restore a single company settings
    public class RestoreCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public RestoreCompanySettingsCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
