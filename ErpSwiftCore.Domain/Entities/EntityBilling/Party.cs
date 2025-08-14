using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class Party : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public PartyType Type { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }

        // روابط اختيارية للمزيد من المعلومات
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public Guid? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // روابط الطلبات والفواتير
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}