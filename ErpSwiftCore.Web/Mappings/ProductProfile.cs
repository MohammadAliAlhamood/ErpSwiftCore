using AutoMapper;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels;

namespace ErpSwiftCore.Web.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        { 
            CreateMap<ProductDto, ProductCreateDto>().ReverseMap();
            CreateMap<ProductDto, ProductUpdateDto>().ReverseMap();

            CreateMap<ProductUnitConversionDto, ProductUnitConversionCreateDto>().ReverseMap();
            CreateMap<ProductUnitConversionDto, ProductUnitConversionUpdateDto>().ReverseMap();


            CreateMap<ProductTaxDto, ProductTaxCreateDto>().ReverseMap();
            CreateMap<ProductTaxDto, ProductTaxUpdateDto>().ReverseMap();


            CreateMap<ProductCategoryDto, ProductCategoryCreateDto>().ReverseMap();
            CreateMap<ProductCategoryDto, ProductCategoryUpdateDto>().ReverseMap();

            CreateMap<ProductPriceDto, ProductPriceCreateDto>().ReverseMap();
            CreateMap<ProductPriceDto, ProductPriceUpdateDto>().ReverseMap();

            CreateMap<ProductBundleDto, ProductBundleCreateDto>().ReverseMap();
            CreateMap<ProductBundleDto, ProductBundleUpdateDto>().ReverseMap();


        }
    }
}