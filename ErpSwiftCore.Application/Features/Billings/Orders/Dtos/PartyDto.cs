using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Domain.Enums; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
{


    public class PartyDto : AuditableEntityDto
    {
        public string Name { get; set; } = null!;
        public PartyType Type { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }

        /// <summary>
        /// إذا كان هذا الطرف مرتبطًا بعميل
        /// </summary>
        public Guid? CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public string? CustomerCode { get; set; }

        /// <summary>
        /// إذا كان هذا الطرف مرتبطًا بمورد
        /// </summary>
        public Guid? SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }
        public string? SupplierCode { get; set; }
    }

}
