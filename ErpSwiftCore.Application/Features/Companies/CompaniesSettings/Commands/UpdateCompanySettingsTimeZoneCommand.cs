using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Update only timezone in settings
    public class UpdateCompanySettingsTimeZoneCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsTimeZoneUpdateDto Payload { get; }

        public UpdateCompanySettingsTimeZoneCommand(CompanySettingsTimeZoneUpdateDto payload)
        {
            Payload = payload;
        }
    }

}
