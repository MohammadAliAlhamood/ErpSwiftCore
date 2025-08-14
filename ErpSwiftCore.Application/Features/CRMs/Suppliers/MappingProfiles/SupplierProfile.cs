using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Domain.Entities.EntityCRM; 

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            CreateMap<Supplier, UpdateSupplierDto>().ReverseMap();
        }
    }
}