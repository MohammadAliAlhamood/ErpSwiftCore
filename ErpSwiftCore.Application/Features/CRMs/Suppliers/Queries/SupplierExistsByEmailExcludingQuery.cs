using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 4. בדיקת קיום ע״פ מייל עם استثناء מזהה
    public class SupplierExistsByEmailExcludingQuery : IRequest<APIResponseDto>
    {
        public string Email { get; }
        public Guid ExcludingId { get; }
        public SupplierExistsByEmailExcludingQuery(string email, Guid excludingId)
        {
            Email = email;
            ExcludingId = excludingId;
        }
    }
}
