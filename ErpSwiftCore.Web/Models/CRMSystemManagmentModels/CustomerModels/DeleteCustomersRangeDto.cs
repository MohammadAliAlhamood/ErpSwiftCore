namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels
{

    public class DeleteCustomersRangeDto
    {
        public IEnumerable<Guid> CustomerIds { get; set; } = new List<Guid>();
    }

}
