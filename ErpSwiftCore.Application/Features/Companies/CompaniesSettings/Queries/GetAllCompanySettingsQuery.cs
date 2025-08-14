using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Queries
{


    #region ──────────── CompanySettings Queries ────────────

    public class GetAllCompanySettingsQuery : IRequest<APIResponseDto> { }

    public class GetActiveCompanySettingsQuery : IRequest<APIResponseDto> { }

    public class GetSoftDeletedCompanySettingsQuery : IRequest<APIResponseDto> { }

    public class GetCompanySettingsByCompanyIdQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetCompanySettingsByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetCompanySettingsByCurrencyQuery : IRequest<APIResponseDto>
    {
        public Guid CurrencyId { get; }

        public GetCompanySettingsByCurrencyQuery(Guid currencyId)
        {
            CurrencyId = currencyId;
        }
    }

    public class GetCompanySettingsByTimeZoneQuery : IRequest<APIResponseDto>
    {
        public string TimeZone { get; }

        public GetCompanySettingsByTimeZoneQuery(string timeZone)
        {
            TimeZone = timeZone;
        }
    }

    public class GetCompanySettingsPagedQuery : IRequest<APIResponseDto>
    {
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompanySettingsPagedQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class GetCompanySettingsPagedByCurrencyQuery : IRequest<APIResponseDto>
    {
        public Guid CurrencyId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompanySettingsPagedByCurrencyQuery(Guid currencyId, int pageIndex, int pageSize)
        {
            CurrencyId = currencyId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class GetCompanySettingsPagedByTimeZoneQuery : IRequest<APIResponseDto>
    {
        public string TimeZone { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetCompanySettingsPagedByTimeZoneQuery(string timeZone, int pageIndex, int pageSize)
        {
            TimeZone = timeZone;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class SearchCompanySettingsByKeywordQuery : IRequest<APIResponseDto>
    {
        public string Keyword { get; }

        public SearchCompanySettingsByKeywordQuery(string keyword)
        {
            Keyword = keyword;
        }
    }

    public class SettingsExistQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public SettingsExistQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class IsCompanySettingsUniqueQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public IsCompanySettingsUniqueQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetCompanySettingsCountQuery : IRequest<APIResponseDto> { }

    public class GetActiveCompanySettingsCountQuery : IRequest<APIResponseDto> { }

    #endregion
}






