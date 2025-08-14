using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICRMsService;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Service.CRMsService
{
    public class SupplierService : ISupplierService
    {
        private readonly IBaseService _baseService;

        public SupplierService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetAllAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier"
            });

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/{id}"
            });

        public async Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/supplier/by-ids"
            });

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckExistsAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/{id}"
            });

        public async Task<APIResponseDto?> CheckExistsByCodeAsync(string supplierCode) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/code/{supplierCode}"
            });

        public async Task<APIResponseDto?> CheckExistsByEmailAsync(string email) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/email?email={Uri.EscapeDataString(email)}"
            });

        public async Task<APIResponseDto?> CheckExistsByEmailExcludingAsync(string email, Guid excludingId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/email/{excludingId}?email={Uri.EscapeDataString(email)}"
            });

        public async Task<APIResponseDto?> CheckExistsByNationalIdAsync(string nationalId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/nationalId?nationalId={Uri.EscapeDataString(nationalId)}"
            });

        public async Task<APIResponseDto?> CheckExistsByNationalIdExcludingAsync(string nationalId, Guid excludingId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/nationalId/{excludingId}?nationalId={Uri.EscapeDataString(nationalId)}"
            });

        public async Task<APIResponseDto?> CheckExistsByPhoneAsync(string phone) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/phone?phone={Uri.EscapeDataString(phone)}"
            });

        public async Task<APIResponseDto?> CheckExistsByPhoneExcludingAsync(string phone, Guid excludingId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/exists/phone/{excludingId}?phone={Uri.EscapeDataString(phone)}"
            });

        public async Task<APIResponseDto?> CheckIsUniqueAsync(
            string code, string? email, string nationalId, string? phone) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/unique" +
                          $"?code={Uri.EscapeDataString(code)}" +
                          $"&email={Uri.EscapeDataString(email ?? string.Empty)}" +
                          $"&nationalId={Uri.EscapeDataString(nationalId)}" +
                          $"&phone={Uri.EscapeDataString(phone ?? string.Empty)}"
            });

        public async Task<APIResponseDto?> CheckIsUniqueExcludingAsync(
            Guid excludingId, string code, string? email, string nationalId, string? phone) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/supplier/unique/{excludingId}" +
                          $"?code={Uri.EscapeDataString(code)}" +
                          $"&email={Uri.EscapeDataString(email ?? string.Empty)}" +
                          $"&nationalId={Uri.EscapeDataString(nationalId)}" +
                          $"&phone={Uri.EscapeDataString(phone ?? string.Empty)}"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> CreateAsync(CreateSupplierDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/supplier/create"
            });

        public async Task<APIResponseDto?> UpdateAsync(UpdateSupplierDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/supplier/update"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/supplier/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/supplier/delete-range"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/supplier/restore/{id}"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/supplier/restore-range"
            });
    }
}
