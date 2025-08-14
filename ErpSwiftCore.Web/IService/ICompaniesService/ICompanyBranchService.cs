using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.ICompaniesService
{
    public interface ICompanyBranchService
    {
        Task<APIResponseDto?> CreateBranchAsync(CompanyBranchCreateDto dto);
        Task<APIResponseDto?> UpdateBranchAsync(CompanyBranchUpdateDto dto);
        Task<APIResponseDto?> DeleteBranchAsync(Guid branchId);
        Task<APIResponseDto?> DeleteAllBranchesAsync(Guid companyId);
        Task<APIResponseDto?> GetAllBranchesAsync();
        Task<APIResponseDto?> GetBranchByIdAsync(Guid branchId);
        Task<APIResponseDto?> GetBranchWithCompanyAsync(Guid branchId);
        Task<APIResponseDto?> GetBranchesByCompanyIdAsync(Guid companyId);
        Task<APIResponseDto?> BranchExistsAsync(Guid branchId);
        Task<APIResponseDto?> BranchExistsByCodeAsync(Guid companyId, string branchCode);
        Task<APIResponseDto?> IsBranchNameUniqueAsync(Guid companyId, string branchName, Guid? excludeBranchId);
        Task<APIResponseDto?> HasBranchesAsync(Guid companyId);
        Task<APIResponseDto?> GetBranchesCountAsync(Guid companyId);
    }
}
