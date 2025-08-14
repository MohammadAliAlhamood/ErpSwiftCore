using ErpSwiftCore.Domain.Entities.EntityFinancial; 
namespace ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService
{
    public interface IFinancialReportValidationService
    {
        Task<bool> ValidateReportAsync(CustomFinancialReportResult report, CancellationToken cancellationToken = default);
    }
}
