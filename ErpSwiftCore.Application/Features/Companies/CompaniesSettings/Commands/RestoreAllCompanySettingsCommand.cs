using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands
{
    // Restore all SoftDeleted settings
    public class RestoreAllCompanySettingsCommand : IRequest<APIResponseDto> { }


}
