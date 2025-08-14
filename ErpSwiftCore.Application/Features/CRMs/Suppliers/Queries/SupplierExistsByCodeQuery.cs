using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries
{
    // 2. בדיקת קיום ע״פ קוד ספק
    public class SupplierExistsByCodeQuery : IRequest<APIResponseDto>
    {
        public string SupplierCode { get; }
        public SupplierExistsByCodeQuery(string supplierCode) => SupplierCode = supplierCode;
    }
}
