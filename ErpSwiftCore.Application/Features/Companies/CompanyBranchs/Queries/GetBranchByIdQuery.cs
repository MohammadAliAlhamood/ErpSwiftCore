using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Queries
{

    #region ──────────── CompanyBranch Queries ────────────

    public class GetBranchByIdQuery : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }

        public GetBranchByIdQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }

    public class GetBranchWithCompanyQuery : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }

        public GetBranchWithCompanyQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }

    public class GetAllBranchesQuery : IRequest<APIResponseDto> { }

    public class GetBranchesByCompanyIdQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetBranchesByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetActiveBranchesByCompanyIdQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetActiveBranchesByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetSoftDeletedBranchesByCompanyIdQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetSoftDeletedBranchesByCompanyIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetBranchesPagedQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetBranchesPagedQuery(Guid companyId, int pageIndex, int pageSize)
        {
            CompanyId = companyId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class GetActiveBranchesPagedQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetActiveBranchesPagedQuery(Guid companyId, int pageIndex, int pageSize)
        {
            CompanyId = companyId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class GetSoftDeletedBranchesPagedQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public int PageIndex { get; }
        public int PageSize { get; }

        public GetSoftDeletedBranchesPagedQuery(Guid companyId, int pageIndex, int pageSize)
        {
            CompanyId = companyId;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class SearchBranchesByNameQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public string Name { get; }

        public SearchBranchesByNameQuery(Guid companyId, string name)
        {
            CompanyId = companyId;
            Name = name;
        }
    }

    public class SearchBranchesByCodeQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public string Code { get; }

        public SearchBranchesByCodeQuery(Guid companyId, string code)
        {
            CompanyId = companyId;
            Code = code;
        }
    }

    public class SearchBranchesByKeywordQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public string Keyword { get; }

        public SearchBranchesByKeywordQuery(Guid companyId, string keyword)
        {
            CompanyId = companyId;
            Keyword = keyword;
        }
    }

    public class BranchExistsQuery : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }

        public BranchExistsQuery(Guid branchId)
        {
            BranchId = branchId;
        }
    }

    public class BranchExistsByCodeQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public string BranchCode { get; }

        public BranchExistsByCodeQuery(Guid companyId, string branchCode)
        {
            CompanyId = companyId;
            BranchCode = branchCode;
        }
    }

    public class IsBranchNameUniqueQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public string BranchName { get; }
        public Guid? ExcludeBranchId { get; }

        public IsBranchNameUniqueQuery(Guid companyId, string branchName, Guid? excludeBranchId = null)
        {
            CompanyId = companyId;
            BranchName = branchName;
            ExcludeBranchId = excludeBranchId;
        }
    }

    public class HasBranchesQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public HasBranchesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetBranchesCountQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetBranchesCountQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class GetActiveBranchesCountQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public GetActiveBranchesCountQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    #endregion

}
