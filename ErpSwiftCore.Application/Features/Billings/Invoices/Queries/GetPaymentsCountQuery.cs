using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Queries
{
    /// <summary>
    /// 10. عدّ الدفعات المرتبطة بفاتورة
    /// </summary>
    public class GetPaymentsCountQuery : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public GetPaymentsCountQuery(Guid invoiceId) => InvoiceId = invoiceId;
    }
}
