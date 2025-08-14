using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility; 

namespace ErpSwiftCore.Web.Service.FinancialsService
{
    public class AccountService : IAccountService
    {
        private readonly IBaseService _baseService;

        public AccountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/{id}"
            });

        public async Task<APIResponseDto?> GetSoftDeletedByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/soft-deleted/{id}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/all"
            });

        public async Task<APIResponseDto?> GetByParentAsync(Guid parentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/by-parent/{parentId}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/account/by-ids"
            });

        public async Task<APIResponseDto?> GetByTransactionTypeAsync(TransactionType type) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/by-transaction-type/{type}"
            });

        public async Task<APIResponseDto?> GetHierarchyAsync(Guid rootId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/hierarchy/{rootId}"
            });

        public async Task<APIResponseDto?> GetWithParentAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/with-parent/{id}"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/count"
            });

        public async Task<APIResponseDto?> GetCountByTypeAsync(TransactionType type) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/count/by-type/{type}"
            });

        public async Task<APIResponseDto?> GetTotalBalanceByTypeAsync(TransactionType type) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/balance/by-type/{type}"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateAccountDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/account/create"
            });

        public async Task<APIResponseDto?> AddRangeAsync(AddAccountsRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/account/add-range"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/account/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/account/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/account/delete-all"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateAccountDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/account/update"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/account/restore/{id}"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/account/restore-range"
            });

        public async Task<APIResponseDto?> RestoreAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/account/restore-all"
            });

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/validate/exists/{id}"
            });

       

        public async Task<APIResponseDto?> ValidateParentAsync(Guid parentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/account/validate/parent/{parentId}"
            });
    }
}
