namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{

    public class CreatePaymentDto
    {
        public string? PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsReconciled { get; set; }
    }

}
