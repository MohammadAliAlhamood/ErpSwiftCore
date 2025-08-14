using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IInventoriesService
{
    public interface IInventoryPolicyService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetByInventoryAsync(Guid inventoryId);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByTypeAsync(InventoryPolicyType policyType);
        Task<APIResponseDto?> GetWithAutoReorderAsync();
        Task<APIResponseDto?> GetBelowReorderAsync();
        Task<APIResponseDto?> GetAboveMaxAsync();
        Task<APIResponseDto?> GetCountAsync();

        // Commands
        Task<APIResponseDto?> EnableAutoReorderAsync(EnableAutoReorderDto dto);
        Task<APIResponseDto?> DisableAutoReorderAsync(DisableAutoReorderDto dto);
        Task<APIResponseDto?> UpdatePolicyAsync(UpdatePolicyDto dto);
        Task<APIResponseDto?> UpdateReorderLevelAsync(UpdateReorderLevelDto dto);
        Task<APIResponseDto?> UpdateMaxStockLevelAsync(UpdateMaxStockLevelDto dto);
        Task<APIResponseDto?> UpdateRangeAsync(UpdatePoliciesRangeDto dto);
    }
}
