namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels
{ 
    public class RestoreSuppliersRangeDto
    {
        public IEnumerable<Guid> SupplierIds { get; set; } = new List<Guid>();
    } 
}
