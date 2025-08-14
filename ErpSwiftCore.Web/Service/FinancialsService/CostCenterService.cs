using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.FinancialsService
{
    public class CostCenterService : ICostCenterService
    {
        private readonly IBaseService _baseService;

        public CostCenterService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/{id}"
            });

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/all"
            });

        public async Task<APIResponseDto?> GetByParentAsync(Guid parentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/by-parent/{parentId}"
            });

        public async Task<APIResponseDto?> GetByNameAsync(string name) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/by-name/{name}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/cost-center/by-ids"
            });

        public async Task<APIResponseDto?> GetWithParentAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/with-parent/{id}"
            });

        public async Task<APIResponseDto?> GetWithChildrenAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/with-children/{id}"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/count"
            });

        public async Task<APIResponseDto?> GetCountByParentAsync(Guid parentId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/count/by-parent/{parentId}"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateCostCenterDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/cost-center/create"
            });

        public async Task<APIResponseDto?> AddRangeAsync(AddCostCentersRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/cost-center/add-range"
            });

        public async Task<APIResponseDto?> BulkImportAsync(BulkImportCostCentersDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/cost-center/bulk-import"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateCostCenterDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/cost-center/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/cost-center/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/cost-center/delete-range"
            });

        public async Task<APIResponseDto?> DeleteAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/cost-center/delete-all"
            });

        public async Task<APIResponseDto?> SoftDeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/cost-center/soft-delete/{id}"
            });

        public async Task<APIResponseDto?> SoftDeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/cost-center/soft-delete-range"
            });

        public async Task<APIResponseDto?> SoftDeleteAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/cost-center/soft-delete-all"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/cost-center/restore/{id}"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/cost-center/restore-range"
            });

        public async Task<APIResponseDto?> RestoreAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/cost-center/restore-all"
            });

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/cost-center/validate/exists/{id}"
            });

        

      

    }
}
