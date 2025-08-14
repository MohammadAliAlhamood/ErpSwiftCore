using ErpSwiftCore.Domain.Entities.EntityFinancial;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.FinancialService.FinancialReportService
{
    public class FinancialReportValidationService : IFinancialReportValidationService
    {
        public Task<bool> ValidateReportAsync(CustomFinancialReportResult report, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
