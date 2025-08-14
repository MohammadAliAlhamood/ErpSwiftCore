namespace ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels
{ 
    public class CheckCircularHierarchyDto
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
    } 
}
