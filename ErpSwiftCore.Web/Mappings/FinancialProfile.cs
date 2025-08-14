using AutoMapper;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels;
namespace ErpSwiftCore.Web.Mappings
{
    public class FinancialProfile : Profile
    {
        public FinancialProfile()
        {
            // Customer mappings
            CreateMap<AccountDto, CreateAccountDto>().ReverseMap();
            CreateMap<AccountDto, UpdateAccountDto>().ReverseMap();

            // Supplier mappings
            CreateMap<CostCenterDto, CreateCostCenterDto>().ReverseMap();
            CreateMap<CostCenterDto, UpdateCostCenterDto>().ReverseMap();


             
        }
    }
}
