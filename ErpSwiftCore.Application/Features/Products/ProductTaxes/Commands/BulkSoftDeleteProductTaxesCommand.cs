using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class BulkSoftDeleteProductTaxesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> TaxIds { get; }

        public BulkSoftDeleteProductTaxesCommand(IEnumerable<Guid> taxIds)
        {
            TaxIds = taxIds;
        }
    }
}
