using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{

    public class CreateCompanySettingsCommand : IRequest<APIResponseDto>
    {
        public CompanySettingsCreateDto Settings { get; }

        public CreateCompanySettingsCommand(CompanySettingsCreateDto settings)
        {
            Settings = settings;
        }
    }
     
}