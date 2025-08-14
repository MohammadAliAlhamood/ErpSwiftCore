using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    public class RestoreCompanyRangeCommand : IRequest<APIResponseDto>
    { 
        public IEnumerable<Guid> Ids { get; }

        public RestoreCompanyRangeCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    } 
}
