using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductPriceService
    {
        // ─────────────── Command Methods ───────────────
        Task<APIResponseDto?> CreateAsync(ProductPriceCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductPriceUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid priceId);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();

        // ─────────────── Query Methods ───────────────
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByIdAsync(Guid priceId);
        Task<APIResponseDto?> GetLatestAsync(Guid productId, string priceType);
        Task<APIResponseDto?> GetByProductAsync(Guid productId);
        Task<APIResponseDto?> GetByTypeAsync(string priceType);
        Task<APIResponseDto?> GetByCurrencyAsync(Guid currencyId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByProductAsync(Guid productId);
        Task<APIResponseDto?> GetCountByTypeAsync(string priceType);
        Task<APIResponseDto?> GetWithProductAsync(Guid priceId);
    
    }
}