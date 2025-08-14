using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements
{
    public class UnitOfMeasurementDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
