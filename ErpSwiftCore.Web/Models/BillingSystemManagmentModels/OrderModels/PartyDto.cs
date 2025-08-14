using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
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
