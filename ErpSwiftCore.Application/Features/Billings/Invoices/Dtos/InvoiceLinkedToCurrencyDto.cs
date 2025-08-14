using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.Invoices.Dtos
{

    public class InvoiceLinkedToCurrencyDto
    {
        public Guid InvoiceId { get; set; }
        public Guid CurrencyId { get; set; }
    }

}
