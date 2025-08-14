using ErpSwiftCore.Application.Dtos;
namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos
{
    public class UnitOfMeasurementDto : BaseEntityDto
    { 
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
