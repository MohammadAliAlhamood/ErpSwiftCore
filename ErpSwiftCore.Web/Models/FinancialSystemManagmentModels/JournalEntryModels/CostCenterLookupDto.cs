namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels
{
    /// <summary>
    /// بيانات مختصرة لمركز التكلفة
    /// </summary>
    public class CostCenterLookupDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string? CenterName { get; set; }
    }
}
