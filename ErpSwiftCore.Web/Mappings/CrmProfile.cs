using AutoMapper;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;

namespace ErpSwiftCore.Web.Mappings
{
    public class CrmProfile : Profile
    {
        public CrmProfile()
        {
            // Customer mappings
            CreateMap<CustomerDto, CreateCustomerDto>().ReverseMap();
            CreateMap<CustomerDto, UpdateCustomerDto>().ReverseMap();

            // Supplier mappings
            CreateMap<SupplierDto, CreateSupplierDto>().ReverseMap();
            CreateMap<SupplierDto, UpdateSupplierDto>().ReverseMap();
        }
    }
}