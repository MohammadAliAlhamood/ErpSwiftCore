namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels
{
    public class AddPaymentDto
    {
        public Guid InvoiceId { get; set; }
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
    }

}
