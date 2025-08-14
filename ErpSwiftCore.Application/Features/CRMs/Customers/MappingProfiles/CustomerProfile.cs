using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Domain.Entities.EntityCRM; 
namespace ErpSwiftCore.Application.Features.CRMs.Customers.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        }
    }
}