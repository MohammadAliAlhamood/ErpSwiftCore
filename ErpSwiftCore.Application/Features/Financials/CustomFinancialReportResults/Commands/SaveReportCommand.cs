using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Commands
{
    /// <summary>
    /// حفظ (إنشاء أو تحديث) تقرير مالي مخصص
    /// </summary>
    public class SaveReportCommand : IRequest<APIResponseDto>
    {
        public SaveReportDto Dto { get; }
        public SaveReportCommand(SaveReportDto dto) => Dto = dto;
    }

    /// <summary>
    /// حفظ مجموعة تقارير مالية
    /// </summary>
    public class SaveReportsRangeCommand : IRequest<APIResponseDto>
    {
        public SaveReportsRangeDto Dto { get; }
        public SaveReportsRangeCommand(SaveReportsRangeDto dto) => Dto = dto;
    }

    /// <summary>
    /// حذف تقرير واحد
    /// </summary>
    public class DeleteReportCommand : IRequest<APIResponseDto>
    {
        public Guid ReportId { get; }
        public DeleteReportCommand(Guid reportId) => ReportId = reportId;
    }

    /// <summary>
    /// حذف مجموعة تقارير
    /// </summary>
    public class DeleteReportsRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> ReportIds { get; }
        public DeleteReportsRangeCommand(IEnumerable<Guid> reportIds  ) => ReportIds = reportIds;
    }

    /// <summary>
    /// استرجاع تقرير واحد
    /// </summary>
    public class RestoreReportCommand : IRequest<APIResponseDto>
    {
        public Guid ReportId { get; }
        public RestoreReportCommand(Guid reportId) => ReportId = reportId;
    }
 

    /// <summary>
    /// التحقق من صلاحية تقرير مالي قبل الحفظ
    /// </summary>
    public class ValidateReportCommand : IRequest<APIResponseDto>
    {
        public ValidateReportDto Dto { get; }
        public ValidateReportCommand(ValidateReportDto dto) => Dto = dto;
    }
}