 
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.IFinancialsService
{
    public interface ICostCenterService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByParentAsync(Guid parentId);
        Task<APIResponseDto?> GetByNameAsync(string name);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetWithParentAsync(Guid id);
        Task<APIResponseDto?> GetWithChildrenAsync(Guid id);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByParentAsync(Guid parentId);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateCostCenterDto dto);
        Task<APIResponseDto?> AddRangeAsync(AddCostCentersRangeDto dto);
        Task<APIResponseDto?> BulkImportAsync(BulkImportCostCentersDto dto);
        Task<APIResponseDto?> UpdateAsync(UpdateCostCenterDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> SoftDeleteAsync(Guid id);
        Task<APIResponseDto?> SoftDeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> SoftDeleteAllAsync();
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> RestoreAllAsync();

        // Validation
        Task<APIResponseDto?> CheckExistsAsync(Guid id); 

    }
}
