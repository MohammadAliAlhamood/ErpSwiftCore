using AutoMapper;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;

namespace ErpSwiftCore.Web.Mappings
{
    /// <summary>
    /// AutoMapper profile for UnitOfMeasurement entity and related DTOs.
    /// </summary>
    public class UnitOfMeasurementMappingProfile : Profile
    {
        public UnitOfMeasurementMappingProfile()
        {  
            CreateMap<UnitOfMeasurementDto, UnitOfMeasurementCreateDto>().ReverseMap();
            CreateMap<UnitOfMeasurementDto, UnitOfMeasurementUpdateDto>().ReverseMap();
        }
    }
}