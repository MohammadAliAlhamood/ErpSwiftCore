using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Update entire company settings
    public class UpdateCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsUpdateDto Settings { get; }

        public UpdateCompanySettingsCommand(CompanySettingsUpdateDto settings)
        {
            Settings = settings;
        }
    }

}
