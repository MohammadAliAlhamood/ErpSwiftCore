using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos
{


    #region ──────────── Branch DTOs ────────────

    public class CompanyBranchCreateDto
    {
        public string BranchName { get; set; } = string.Empty;
        public AddressDto? Address { get; set; }
        public ContactInfoDto ContactInfo { get; set; } = new();
        public string BranchCode { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }

    public class CompanyBranchUpdateDto
    {
        public Guid Id { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public AddressDto? Address { get; set; }
        public ContactInfoDto ContactInfo { get; set; } = new();
        public Guid CompanyId { get; set; }
        public string BranchCode { get; internal set; }
    }

    public class CompanyBranchDto
    {
        public Guid Id { get; set; }
        public Guid CompanyID { get; set; }
        public CompanyDto? Company { get; set; }
        public string? BranchName { get; set; }
        public AddressDto? Address { get; set; }
        public ContactInfoDto? ContactInfo { get; set; } = new();
        public string BranchCode { get; set; } = string.Empty;
    }


    public class CompanyBranchPagedResultDto
    {
        public IReadOnlyList<CompanyBranchDto> Branches { get; set; } = new List<CompanyBranchDto>();
        public int TotalCount { get; set; }
    }

    #endregion
    #region ──────────── Search & Filter DTOs ────────────
    public class CompanyBranchSearchByNameDto
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CompanyBranchSearchByCodeDto
    {
        public Guid CompanyId { get; set; }
        public string Code { get; set; } = string.Empty;
    }

    public class CompanyBranchSearchByKeywordDto
    {
        public Guid CompanyId { get; set; }
        public string Keyword { get; set; } = string.Empty;
    }

    #endregion
    #region ──────────── Paging Request DTOs ────────────
    public class CompanyBranchPagedRequestDto
    {
        public Guid CompanyId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class CompanyBranchActivePagedRequestDto
    {
        public Guid CompanyId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class CompanyBranchSoftDeletedPagedRequestDto
    {
        public Guid CompanyId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    #endregion
    #region ──────────── Bulk / State Management DTOs ────────────
    public class CompanyBranchIdsDto
    {
        public IEnumerable<Guid> BranchIds { get; set; } = new List<Guid>();
    }
    public class CompanyBranchActiveStatusDto
    {
        public Guid BranchId { get; set; }
        public bool IsActive { get; set; }
    }
    public class CompanyBranchesActiveStatusRangeDto
    {
        public IEnumerable<Guid> BranchIds { get; set; } = new List<Guid>();
        public bool IsActive { get; set; }
    }

    #endregion
    #region ──────────── Validation / Existence DTOs ────────────
    public class CompanyBranchExistsByCodeDto
    {
        public Guid CompanyId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
    }
    public class CompanyBranchIsNameUniqueDto
    {
        public Guid CompanyId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public Guid? ExcludeBranchId { get; set; }
    }

    #endregion
}
