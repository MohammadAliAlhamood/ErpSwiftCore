using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class PaymentDto : AuditableEntityDto
    {
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsReconciled { get; set; }
        public Guid InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }

    } 
}
