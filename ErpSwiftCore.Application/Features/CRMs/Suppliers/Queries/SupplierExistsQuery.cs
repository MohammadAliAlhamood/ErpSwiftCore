using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    public class SupplierExistsQuery : IRequest<APIResponseDto>
    {
        public Guid SupplierId { get; }
        public SupplierExistsQuery(Guid supplierId) => SupplierId = supplierId;
    }
}
