using ErpSwiftCore.Application; 
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.IBillingsService
{
    public interface IInvoiceService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid invoiceId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> invoiceIds);
        Task<APIResponseDto?> GetCountAsync(InvoiceStatus? status = null);
        Task<APIResponseDto?> GetLinesAsync(Guid invoiceId);
        Task<APIResponseDto?> GetLinesCountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetApprovalsAsync(Guid invoiceId);
        Task<APIResponseDto?> GetApprovalsCountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetApprovalByIdAsync(Guid approvalId);
        Task<APIResponseDto?> GetPaymentsAsync(Guid invoiceId);
        Task<APIResponseDto?> GetPaymentsCountAsync(Guid invoiceId);
        Task<APIResponseDto?> GetPaymentByIdAsync(Guid paymentId);
        Task<APIResponseDto?> CheckExistsAsync(Guid invoiceId);
        Task<APIResponseDto?> CheckLineExistsAsync(Guid lineId);
        Task<APIResponseDto?> CheckApprovalExistsAsync(Guid approvalId);
        Task<APIResponseDto?> CheckPaymentExistsAsync(Guid paymentId);
        Task<APIResponseDto?> IsLinkedToCurrencyAsync(Guid invoiceId, Guid currencyId);
        Task<APIResponseDto?> CalculateTotalAsync(Guid invoiceId);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateInvoiceDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdateInvoiceDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid invoiceId);
        Task<APIResponseDto?> AddLineAsync(Guid invoiceId, CreateInvoiceLineDto dto);
        Task<APIResponseDto?> AddLinesAsync(Guid invoiceId, IEnumerable<CreateInvoiceLineDto> dtos);
        Task<APIResponseDto?> UpdateLineAsync(UpdateInvoiceLineDto dto);
        Task<APIResponseDto?> DeleteLineAsync(Guid lineId);
        Task<APIResponseDto?> DeleteAllLinesAsync(Guid invoiceId);
        Task<APIResponseDto?> AddApprovalAsync(Guid invoiceId, CreateInvoiceApprovalDto dto);
        Task<APIResponseDto?> UpdateApprovalAsync(UpdateInvoiceApprovalDto dto);
        Task<APIResponseDto?> DeleteApprovalAsync(Guid approvalId);
        Task<APIResponseDto?> AddPaymentAsync(Guid invoiceId, CreatePaymentDto dto);
        Task<APIResponseDto?> UpdatePaymentAsync(UpdatePaymentDto dto);
        Task<APIResponseDto?> DeletePaymentAsync(Guid paymentId);
        Task<APIResponseDto?> ChangeStatusAsync(ChangeInvoiceStatusDto dto);
        Task<APIResponseDto?> BulkDeleteAsync(BulkDeleteInvoicesDto dto);
    }
}
