using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductTaxService
    {
        // ─────────────── Command Methods ───────────────
        Task<APIResponseDto?> CreateAsync(ProductTaxCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductTaxUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid taxId);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
        Task<APIResponseDto?> BulkDeleteAsync(IEnumerable<Guid> ids);

        // ─────────────── Query Methods ───────────────
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByIdAsync(Guid taxId);
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
        Task<APIResponseDto?> GetWithProductAsync(Guid taxId);
    }
}
