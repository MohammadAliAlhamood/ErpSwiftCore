using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class SoftDeleteProductTaxCommand : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }

        public SoftDeleteProductTaxCommand(Guid taxId)
        {
            TaxId = taxId;
        }
    }

}
