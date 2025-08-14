using Microsoft.AspNetCore.Http; 
namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos
{
    public class UploadInvoicesFileDto
    {
        public IFormFile File { get; set; } = default!;
    }
}
