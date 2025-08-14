using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Delete multiple companies’ settings
    public class BulkDeleteCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsBulkDeleteDto _Dto { get; }
        public BulkDeleteCompanySettingsCommand(CompanySettingsBulkDeleteDto Dto)
        {
            _Dto = Dto;
        }
    }
}
