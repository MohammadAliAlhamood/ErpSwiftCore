using MediatR; 
using ErpSwiftCore.Application.Features.Billings.Invoices.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.Invoices.Commands
{
    /// <summary>
    /// 16. حذف مجموعة من الفواتير
    /// </summary>
    public class BulkDeleteInvoicesCommand : IRequest<APIResponseDto>
    {
        public   IEnumerable<Guid> InvoiceIds { get; }
        public BulkDeleteInvoicesCommand(IEnumerable<Guid> invoiceIds) => InvoiceIds = invoiceIds;
    }
}
