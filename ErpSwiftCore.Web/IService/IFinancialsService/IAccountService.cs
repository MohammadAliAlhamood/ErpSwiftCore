 
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.IFinancialsService
{
    public interface IAccountService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByParentAsync(Guid parentId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetByTransactionTypeAsync(TransactionType type);
        Task<APIResponseDto?> GetHierarchyAsync(Guid rootId);
        Task<APIResponseDto?> GetWithParentAsync(Guid id);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByTypeAsync(TransactionType type);
        Task<APIResponseDto?> GetTotalBalanceByTypeAsync(TransactionType type);

        // Commands
        Task<APIResponseDto?> CreateAsync(CreateAccountDto dto);
        Task<APIResponseDto?> AddRangeAsync(AddAccountsRangeDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> UpdateAsync(UpdateAccountDto dto);
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> RestoreAllAsync();
         
    }
}
