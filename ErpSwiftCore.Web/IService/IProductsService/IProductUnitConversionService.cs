using System;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductUnitConversionService
    {
        // ─────────────── Command Methods ───────────────
        Task<APIResponseDto?> CreateAsync(ProductUnitConversionCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductUnitConversionUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid conversionId);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();

        // ─────────────── Query Methods ───────────────
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByIdAsync(Guid conversionId);
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByFromUnitAsync(Guid fromUnitId);
        Task<APIResponseDto?> GetByToUnitAsync(Guid toUnitId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetWithProductAsync(Guid conversionId);
        Task<APIResponseDto?> GetWithFromUnitAsync(Guid conversionId);
        Task<APIResponseDto?> GetWithToUnitAsync(Guid conversionId);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
    }
}
