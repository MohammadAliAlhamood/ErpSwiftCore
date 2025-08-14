using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.Accounts.Dtos;
using ErpSwiftCore.Domain.Entities.EntityFinancial; 

namespace ErpSwiftCore.Application.Features.Financials.Accounts.MappingProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>() .ReverseMap(); 
            CreateMap<Account, CreateAccountDto>()   .ReverseMap();
            CreateMap<Account, UpdateAccountDto>()  .ReverseMap();
        }
    }
}







