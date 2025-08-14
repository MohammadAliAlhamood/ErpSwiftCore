using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 7. בדיקת קיום ע״פ טלפון
    public class SupplierExistsByPhoneQuery : IRequest<APIResponseDto>
    {
        public string Phone { get; }
        public SupplierExistsByPhoneQuery(string phone) => Phone = phone;
    }
}
