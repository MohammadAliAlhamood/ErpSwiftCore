namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels
{ 
    public class DeleteSuppliersRangeDto
    {
        public IEnumerable<Guid> SupplierIds { get; set; } = new List<Guid>();
    }
}
