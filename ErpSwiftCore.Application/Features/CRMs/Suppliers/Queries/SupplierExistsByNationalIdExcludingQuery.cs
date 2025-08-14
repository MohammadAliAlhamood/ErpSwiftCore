using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 6. בדיקת קיום ע״פ ת.ז. עם استثناء מזהה
    public class SupplierExistsByNationalIdExcludingQuery : IRequest<APIResponseDto>
    {
        public string NationalId { get; }
        public Guid ExcludingId { get; }
        public SupplierExistsByNationalIdExcludingQuery(string nationalId, Guid excludingId)
        {
            NationalId = nationalId;
            ExcludingId = excludingId;
        }
    }
}
