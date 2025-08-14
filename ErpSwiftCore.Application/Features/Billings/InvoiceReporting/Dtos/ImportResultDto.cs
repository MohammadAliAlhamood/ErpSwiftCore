namespace ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos
{
    public class ImportResultDto
    {
        public int TotalRows { get; set; }
        public int Successful { get; set; }
        public int Failed { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } = Array.Empty<string>();
    } 
}
 
 