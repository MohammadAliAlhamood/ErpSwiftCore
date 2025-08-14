using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{
    public class UpdateInvoiceApprovalDto
    {
        public Guid Id { get; set; }
        public string? Notes { get; set; }
        public InvoiceApprovalStatus Status { get; set; }
    }
}
