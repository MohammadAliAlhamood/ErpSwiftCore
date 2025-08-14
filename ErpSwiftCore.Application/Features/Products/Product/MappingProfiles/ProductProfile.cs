using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Domain.Entities.EntityProduct.Product, 
                ProductDto>().ReverseMap();
            CreateMap<Domain.Entities.EntityProduct.Product, 
                ProductCreateDto>().ReverseMap();
            CreateMap<Domain.Entities.EntityProduct.Product, 
                ProductUpdateDto>().ReverseMap(); 
        }
    }
}
