using ErpSwiftCore.Web.Enums;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels
{
    public class UpdateInvoiceDto
    {
        public Guid Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public InvoiceStatus? InvoiceStatus { get; set; }
        public bool? IsFinalized { get; set; }
        public IEnumerable<CreateInvoiceLineDto>? LinesToAdd { get; set; }
        public IEnumerable<UpdateInvoiceLineDto>? LinesToUpdate { get; set; }
        public IEnumerable<Guid>? LineIdsToDelete { get; set; }
    }


}
