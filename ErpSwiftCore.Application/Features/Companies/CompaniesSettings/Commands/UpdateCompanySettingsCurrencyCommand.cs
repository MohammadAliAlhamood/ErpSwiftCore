using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Update only currency in settings
    public class UpdateCompanySettingsCurrencyCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsCurrencyUpdateDto Payload { get; }

        public UpdateCompanySettingsCurrencyCommand(CompanySettingsCurrencyUpdateDto payload)
        {
            Payload = payload;
        }
    }

}
