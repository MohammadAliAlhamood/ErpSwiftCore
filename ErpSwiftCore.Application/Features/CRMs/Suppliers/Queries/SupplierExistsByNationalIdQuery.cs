using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 5. בדיקת קיום ע״פ ת.ז. לאומית
    public class SupplierExistsByNationalIdQuery : IRequest<APIResponseDto>
    {
        public string NationalId { get; }
        public SupplierExistsByNationalIdQuery(string nationalId) => NationalId = nationalId;
    }
}
