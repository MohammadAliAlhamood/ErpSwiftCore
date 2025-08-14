using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.MappingProfiles
{
    public class ProductUnitConversionProfile : Profile
    {
        public ProductUnitConversionProfile()
        {
            // Entity -> DTO
            CreateMap<ProductUnitConversion, ProductUnitConversionDto>().ReverseMap();
            CreateMap<ProductUnitConversion, ProductUnitConversionCreateDto>().ReverseMap();
            CreateMap<ProductUnitConversion, ProductUnitConversionUpdateDto>().ReverseMap(); 
        }
    }
}
