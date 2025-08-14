using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{

    // Delete all settings
    public class DeleteAllCompanySettingsCommand : IRequest<APIResponseDto> { }

}
