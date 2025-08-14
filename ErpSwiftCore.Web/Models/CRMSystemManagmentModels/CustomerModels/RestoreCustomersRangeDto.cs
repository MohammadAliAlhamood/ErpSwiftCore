namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels
{
    public class RestoreCustomersRangeDto
    {
        public IEnumerable<Guid> CustomerIds { get; set; } = new List<Guid>();
    }
}
