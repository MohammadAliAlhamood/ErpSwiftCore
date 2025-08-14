using ErpSwiftCore.SharedKernel.Base;

namespace ErpSwiftCore.Domain.Entities.EntityFinancial
{
    public class CustomFinancialReportResult : AuditableEntity
    {
        public string ReportName { get; set; } = string.Empty;
        public List<Dictionary<string, object>>? Data { get; set; }
    }
}