using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Commands
{ 
    #region ──────────── CompanyBranch Commands ────────────

    // Create a single branch under a company
    public class CreateCompanyBranchCommand : IRequest<APIResponseDto>
    { 
        public CompanyBranchCreateDto Branch { get; }

        public CreateCompanyBranchCommand(  CompanyBranchCreateDto branch)
        { 
            Branch = branch;
        }
    }

    // Add multiple branches under a company
    public class BulkCreateCompanyBranchesCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public IEnumerable<CompanyBranchCreateDto> Branches { get; }

        public BulkCreateCompanyBranchesCommand(Guid companyId, IEnumerable<CompanyBranchCreateDto> branches)
        {
            CompanyId = companyId;
            Branches = branches;
        }
    }

    // Update an existing branch
    public class UpdateCompanyBranchCommand : IRequest<APIResponseDto>
    { 
        public CompanyBranchUpdateDto Branch { get; }

        public UpdateCompanyBranchCommand(  CompanyBranchUpdateDto branch)
        { 
            Branch = branch;
        }
    }

    // Delete (soft‐delete) a single branch by its ID
    public class DeleteCompanyBranchCommand : IRequest<APIResponseDto>
    { 
        public Guid BranchId { get; }

        public DeleteCompanyBranchCommand(  Guid branchId)
        { 
            BranchId = branchId;
        }
    }

    // Delete multiple branches by their IDs
    public class BulkDeleteCompanyBranchesCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public IEnumerable<Guid> BranchIds { get; }

        public BulkDeleteCompanyBranchesCommand(Guid companyId, IEnumerable<Guid> branchIds)
        {
            CompanyId = companyId;
            BranchIds = branchIds;
        }
    }

    // Delete all branches of a company
    public class DeleteAllCompanyBranchesCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public DeleteAllCompanyBranchesCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    // Restore (un‐archive) a single branch
    public class RestoreCompanyBranchCommand : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }

        public RestoreCompanyBranchCommand(Guid branchId)
        {
            BranchId = branchId;
        }
    }

    // Restore multiple branches
    public class BulkRestoreCompanyBranchesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BranchIds { get; }

        public BulkRestoreCompanyBranchesCommand(IEnumerable<Guid> branchIds)
        {
            BranchIds = branchIds;
        }
    }

    // Restore all branches under a company
    public class RestoreAllCompanyBranchesCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public RestoreAllCompanyBranchesCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    // Set active/inactive status for a single branch
    public class SetCompanyBranchActiveStatusCommand : IRequest<APIResponseDto>
    {
        public Guid BranchId { get; }
        public bool IsActive { get; }

        public SetCompanyBranchActiveStatusCommand(Guid branchId, bool isActive)
        {
            BranchId = branchId;
            IsActive = isActive;
        }
    }

    // Set active/inactive status for multiple branches
    public class BulkSetCompanyBranchesActiveStatusCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> BranchIds { get; }
        public bool IsActive { get; }

        public BulkSetCompanyBranchesActiveStatusCommand(IEnumerable<Guid> branchIds, bool isActive)
        {
            BranchIds = branchIds;
            IsActive = isActive;
        }
    }

    #endregion

}
