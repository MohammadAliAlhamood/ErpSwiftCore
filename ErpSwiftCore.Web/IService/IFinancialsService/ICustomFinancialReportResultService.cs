using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CustomFinancialReportResultModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels; 
namespace ErpSwiftCore.Web.IService.IFinancialsService
{
    public interface ICustomFinancialReportResultService
    {
        // Queries
        Task<APIResponseDto?> GetByIdAsync(Guid id);
        Task<APIResponseDto?> GetAllAsync(bool includeDeleted = false);
        Task<APIResponseDto?> GetByCompanyAsync(Guid companyId);
        Task<APIResponseDto?> ExportToExcelAsync(Guid id);
        Task<APIResponseDto?> ExportToCsvAsync(Guid id);
        Task<APIResponseDto?> GetRecentAsync(int topCount);
        Task<APIResponseDto?> GetCountByCompanyAsync();
        Task<APIResponseDto?> GetCountAsync();


        // Commands
        Task<APIResponseDto?> SaveAsync(SaveReportDto dto); 
        Task<APIResponseDto?> DeleteAsync(Guid id);
        Task<APIResponseDto?> DeleteRangeAsync(DeleteReportsRangeDto dto);
        Task<APIResponseDto?> RestoreAsync(Guid id);
        Task<APIResponseDto?> ValidateAsync(ValidateReportDto dto);
    }
}
