
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IBillingsService
{
    public interface IPaymentService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetByInvoiceAsync(Guid invoiceId);
        Task<APIResponseDto?> GetCountAsync(Guid invoiceId);

        // Commands
        Task<APIResponseDto?> CreateAsync(AddPaymentDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdatePaymentDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);

        // Validation
        Task<APIResponseDto?> CheckExistsAsync(Guid id);
    }
}
