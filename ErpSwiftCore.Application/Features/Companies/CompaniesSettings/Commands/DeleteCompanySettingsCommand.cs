using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Delete (soft‐delete) a single company's settings
    public class DeleteCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public DeleteCompanySettingsCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }


}
