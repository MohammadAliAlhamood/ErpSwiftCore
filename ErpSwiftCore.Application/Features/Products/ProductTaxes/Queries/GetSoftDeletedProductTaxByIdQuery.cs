using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class GetSoftDeletedProductTaxByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }

        public GetSoftDeletedProductTaxByIdQuery(Guid taxId)
        {
            TaxId = taxId;
        }
    }

}
