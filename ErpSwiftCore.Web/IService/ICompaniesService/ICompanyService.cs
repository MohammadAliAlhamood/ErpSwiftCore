using System;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.ICompaniesService
{
    public interface ICompanyService
    {
        Task<APIResponseDto?> CreateCompanyAsync(CompanyCreateDto dto);
        Task<APIResponseDto?> BulkCreateCompaniesAsync(IEnumerable<CompanyCreateDto> dto);
        Task<APIResponseDto?> UpdateCompanyAsync(CompanyUpdateDto dto);
        Task<APIResponseDto?> DeleteCompanyAsync(Guid companyId);
        Task<APIResponseDto?> DeleteAllCompaniesAsync();
        Task<APIResponseDto?> GetAllCompaniesAsync();
        Task<APIResponseDto?> GetCompanyByIdAsync(Guid companyId);
        Task<APIResponseDto?> GetCompaniesCountAsync();
    }
}
