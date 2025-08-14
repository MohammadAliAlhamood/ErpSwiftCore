using AutoMapper;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryTransactionModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.StocksTransferModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
namespace ErpSwiftCore.Web.Mappings
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {


            // Inventory with Policy
            CreateMap<InventoryDto, InventoryWithPolicyDto>().ReverseMap();

            // Inventory with Transactions
            CreateMap<InventoryDto, InventoryWithTransactionsDto>().ReverseMap();

            // Inventory with Notifications
            CreateMap<InventoryDto, InventoryWithNotificationsDto>().ReverseMap();

            // Full detail: Inventory + Policy + Transactions + Notifications
            CreateMap<InventoryDto, InventoryFullDetailDto>().ReverseMap();

            // Nested mappings
            CreateMap<InventoryPolicyDto, InventoryPolicyDto>().ReverseMap();
            CreateMap<InventoryTransactionDto, InventoryTransactionDto>().ReverseMap();



            // Create/Update full policy
            CreateMap<UpdatePolicyDto, InventoryPolicyDto>().ReverseMap();
            CreateMap<UpdateReorderLevelDto, InventoryPolicyDto>().ReverseMap();
            // Update max stock level only
            CreateMap<UpdateMaxStockLevelDto, InventoryPolicyDto>().ReverseMap();

            // Batch update policies
            CreateMap<UpdatePoliciesRangeDto, IEnumerable<InventoryPolicyDto>>().ReverseMap();







            // With product details
            CreateMap<StockTransferDto, StockTransferWithProductDto>().ReverseMap();

            // With warehouse details
            CreateMap<StockTransferDto, StockTransferWithWarehousesDto>().ReverseMap();

            // Full detail: product + both warehouses
            CreateMap<StockTransferDto, StockTransferFullDetailDto>().ReverseMap();

            // Search results & paging


            // Last transfer
            CreateMap<StockTransferDto, LastStockTransferDto>().ReverseMap();

            // Create/Update DTO → Entity
            CreateMap<CreateStockTransferDto, StockTransferDto>().ReverseMap();

            CreateMap<UpdateStockTransferDto, StockTransferDto>().ReverseMap();
            CreateMap<WarehouseDto, WarehouseWithBranchDto>().ReverseMap();

            CreateMap<WarehouseDto, WarehouseWithInventoriesDto>().ReverseMap();



            // RecentWarehouseDto reuses WarehouseDto mapping
            CreateMap<WarehouseDto, RecentWarehouseDto>().ReverseMap();

            // Create/Update DTOs → Entity
            CreateMap<CreateWarehouseDto, WarehouseDto>().ReverseMap();

            CreateMap<UpdateWarehouseDto, WarehouseDto>().ReverseMap();

        }
    }
}