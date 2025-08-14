using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.MappingProfiles
{
    public class ProductPriceProfile : Profile
    {
        public ProductPriceProfile()
        { 
            CreateMap<ProductPrice, ProductPriceDto>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceCreateDto>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceUpdateDto>().ReverseMap();
        }
    }
}
