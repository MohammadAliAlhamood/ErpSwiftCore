using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 8. בדיקת קיום ע״פ טלפון עם استثناء מזהה
    public class SupplierExistsByPhoneExcludingQuery : IRequest<APIResponseDto>
    {
        public string Phone { get; }
        public Guid ExcludingId { get; }
        public SupplierExistsByPhoneExcludingQuery(string phone, Guid excludingId)
        {
            Phone = phone;
            ExcludingId = excludingId;
        }
    }
}
