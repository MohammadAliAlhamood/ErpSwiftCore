using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.MappingProfiles
{
    public class ProductBundleProfile : Profile
    {
        public ProductBundleProfile()
        {
            // Entity -> DTO
            CreateMap<ProductBundle, ProductBundleDto>().ReverseMap();
            CreateMap<ProductBundle, ProductBundleWithRelationsDto>().ReverseMap();
            CreateMap<ProductBundle, ProductBundleCreateDto>().ReverseMap();
            CreateMap<ProductBundle, ProductBundleUpdateDto>().ReverseMap();
        }
    }
}
