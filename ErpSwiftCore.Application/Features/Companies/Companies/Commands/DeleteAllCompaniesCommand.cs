using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Delete all companies
    public class DeleteAllCompaniesCommand : IRequest<APIResponseDto> { }
}
