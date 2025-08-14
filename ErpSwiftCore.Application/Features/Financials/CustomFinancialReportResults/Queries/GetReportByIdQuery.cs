using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Queries
{
    /// <summary>
    /// الحصول على تقرير مالي مخصص حسب المعرف
    /// </summary>
    public class GetReportByIdQuery : IRequest<APIResponseDto>
    {
        public Guid ReportId { get; }
        public GetReportByIdQuery(Guid reportId) => ReportId = reportId;
    }

    /// <summary>
    /// الحصول على جميع التقارير (اختياري: تضمين المحذوفة)
    /// </summary>
    public class GetAllReportsQuery : IRequest<APIResponseDto>
    {
        public bool IncludeDeleted { get; }
        public GetAllReportsQuery(bool includeDeleted = false) => IncludeDeleted = includeDeleted;
    }

    /// <summary>
    /// الحصول على التقارير الخاصة بشركة
    /// </summary>
    public class GetReportsByCompanyQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public GetReportsByCompanyQuery(Guid companyId) => CompanyId = companyId;
    }

    /// <summary>
    /// تصدير تقرير إلى Excel (بايت مصفوفة)
    /// </summary>
    public class ExportReportToExcelQuery : IRequest<APIResponseDto>
    {
        public Guid ReportId { get; }
        public ExportReportToExcelQuery(Guid reportId) => ReportId = reportId;
    }

    /// <summary>
    /// تصدير تقرير إلى CSV (نص)
    /// </summary>
    public class ExportReportToCsvQuery : IRequest<APIResponseDto>
    {
        public Guid ReportId { get; }
        public ExportReportToCsvQuery(Guid reportId) => ReportId = reportId;
    }

    /// <summary>
    /// الحصول على أحدث التقارير بعدد محدد
    /// </summary>
    public class GetRecentReportsQuery : IRequest<APIResponseDto>
    {
        public int TopCount { get; }
        public GetRecentReportsQuery(int topCount) => TopCount = topCount;
    }

    /// <summary>
    /// إحصائية: عدد التقارير لكل شركة
    /// </summary>
    public class GetReportsCountByCompanyQuery : IRequest<APIResponseDto> { }

    /// <summary>
    /// إحصائية: العدد الكلي للتقارير
    /// </summary>
    public class GetReportsCountQuery : IRequest<APIResponseDto> { }

 
}
