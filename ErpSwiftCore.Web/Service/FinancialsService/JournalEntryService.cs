using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.FinancialsService
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IBaseService _baseService;

        public JournalEntryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id, bool includeLines = false) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/{id}?includeLines={includeLines}"
            });

        public async Task<APIResponseDto?> GetByAccountAsync(Guid accountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/by-account/{accountId}"
            });

        public async Task<APIResponseDto?> GetByDateRangeAsync(DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/by-date-range?from={from:O}&to={to:O}"
            });

        public async Task<APIResponseDto?> GetLinesAsync(Guid journalEntryId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/lines/{journalEntryId}"
            });

        public async Task<APIResponseDto?> GetSumDebitAsync(Guid accountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/sum/debit/{accountId}"
            });

        public async Task<APIResponseDto?> GetSumCreditAsync(Guid accountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/sum/credit/{accountId}"
            });

        public async Task<APIResponseDto?> GetTotalAsync(Guid accountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/total/{accountId}"
            });

        public async Task<APIResponseDto?> ReconcileAsync(Guid accountId, DateTime from, DateTime to) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/reconcile/{accountId}?from={from:O}&to={to:O}"
            });

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> CheckReferenceExistsAsync(string referenceNumber) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/validate/reference/{referenceNumber}"
            });

        public async Task<APIResponseDto?> CheckLineExistsByAccountAsync(Guid accountId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/validate/line-exists/{accountId}"
            });

        // ─────────────── Command Methods ───────────────

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/delete-range"
            });

        public async Task<APIResponseDto?> SoftDeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/soft-delete/{id}"
            });

        public async Task<APIResponseDto?> SoftDeleteRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/soft-delete-range"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/restore/{id}"
            });

        public async Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = ids,
                Url = $"{SD.ErpAPIBase}/api/journal-entry/restore-range"
            });
    }
}
