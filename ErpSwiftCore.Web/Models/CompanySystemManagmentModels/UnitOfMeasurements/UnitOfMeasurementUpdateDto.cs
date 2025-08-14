namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements
{
    public class UnitOfMeasurementUpdateDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
