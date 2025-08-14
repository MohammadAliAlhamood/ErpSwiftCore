using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.MappingProfiles
{
    public class ProductTaxProfile : Profile
    {
        public ProductTaxProfile()
        {
            // Entity -> DTO
            CreateMap<ProductTax, ProductTaxDto>().ReverseMap();
            CreateMap<ProductTax, ProductTaxCreateDto>().ReverseMap();
            CreateMap<ProductTax, ProductTaxUpdateDto>().ReverseMap();
        }
    }
}
