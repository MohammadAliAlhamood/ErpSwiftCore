namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{
    public class UpdatePaymentDto
    {
        public Guid Id { get; set; }
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Guid InvoiceId { get; set; }

        public bool IsReconciled { get; set; }
    }

}
