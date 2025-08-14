using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Add multiple settings records
    public class BulkCreateCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsBulkCreateDto _Dto { get; }
        public BulkCreateCompanySettingsCommand(CompanySettingsBulkCreateDto Dto)
        {
            _Dto = Dto;
        }
    }


}
