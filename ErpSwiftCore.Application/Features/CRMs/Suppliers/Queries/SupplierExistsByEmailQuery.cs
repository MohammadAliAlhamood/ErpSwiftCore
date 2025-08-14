using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 3. בדיקת קיום ע״פ מייל
    public class SupplierExistsByEmailQuery : IRequest<APIResponseDto>
    {
        public string Email { get; }
        public SupplierExistsByEmailQuery(string email) => Email = email;
    }
}
