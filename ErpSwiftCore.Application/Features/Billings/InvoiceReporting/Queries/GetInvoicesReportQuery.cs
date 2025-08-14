using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries
{
    /// <summary>4. Get invoice report with optional filter</summary>
    public class GetInvoicesReportQuery : IRequest<APIResponseDto>
    {
        public InvoiceReportFilterDto Filter { get; }
        public GetInvoicesReportQuery(InvoiceReportFilterDto filter) => Filter = filter;
    } 
}
