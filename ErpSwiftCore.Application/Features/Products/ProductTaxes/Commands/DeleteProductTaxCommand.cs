using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands
{
    public class DeleteProductTaxCommand : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }

        public DeleteProductTaxCommand(Guid taxId)
        {
            TaxId = taxId;
        }
    }
}
