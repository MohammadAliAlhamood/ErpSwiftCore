using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels;

namespace ErpSwiftCore.Web.Service.FinancialsService
{
    public class CustomFinancialReportResultService : ICustomFinancialReportResultService
    {
        private readonly IBaseService _baseService;

        public CustomFinancialReportResultService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // ─────────────── Query Methods ───────────────

        public async Task<APIResponseDto?> GetByIdAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/{id}"
            });

        public async Task<APIResponseDto?> GetAllAsync(bool includeDeleted = false) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/all?includeDeleted={includeDeleted}"
            });

        public async Task<APIResponseDto?> GetByCompanyAsync(Guid companyId) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/by-company/{companyId}"
            });

        public async Task<APIResponseDto?> GetRecentAsync(int topCount) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/recent/{topCount}"
            });

        public async Task<APIResponseDto?> GetCountByCompanyAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/count/by-company"
            });

        public async Task<APIResponseDto?> GetCountAsync() =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/count"
            });

        // ─────────────── Command Methods ───────────────

        

        public async Task<APIResponseDto?> SaveAsync(SaveReportDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/save"
            });

       
        public async Task<APIResponseDto?> ExportToExcelAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/export/excel/{id}"
            });

        public async Task<APIResponseDto?> ExportToCsvAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/export/csv/{id}"
            });

        public async Task<APIResponseDto?> DeleteAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/delete/{id}"
            });

        public async Task<APIResponseDto?> DeleteRangeAsync(DeleteReportsRangeDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/delete-range"
            });

        public async Task<APIResponseDto?> RestoreAsync(Guid id) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/restore/{id}"
            });

        // ─────────────── Validation Methods ───────────────

        public async Task<APIResponseDto?> ValidateAsync(ValidateReportDto dto) =>
            await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{SD.ErpAPIBase}/api/custom-financial-report-result/validate"
            });
    }
}
