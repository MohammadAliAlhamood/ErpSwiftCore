using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Restore multiple companies’ settings
    public class BulkRestoreCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsBulkRestoreDto _Dto { get; }
        public BulkRestoreCompanySettingsCommand(CompanySettingsBulkRestoreDto Dto)
        {
            _Dto = Dto;
        }
    }
}
