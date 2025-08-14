using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class UpdatePaymentDto
    {
        public Guid Id { get; set; }
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsReconciled { get; set; }
    }
}
