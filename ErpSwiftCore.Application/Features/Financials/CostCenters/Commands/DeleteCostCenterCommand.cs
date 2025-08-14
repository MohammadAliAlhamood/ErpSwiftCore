using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CostCenters.Commands
{
    public class DeleteCostCenterCommand : IRequest<APIResponseDto>
    {
        public Guid CenterId { get; }
        public DeleteCostCenterCommand(Guid centerId) => CenterId = centerId;
    }
}
