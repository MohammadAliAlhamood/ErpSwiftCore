using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.MappingProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            // Entity -> DTO
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<(int active, int inactive), CategoryCountByStatusDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryCreateDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryUpdateDto>().ReverseMap();
        }
    }
}
