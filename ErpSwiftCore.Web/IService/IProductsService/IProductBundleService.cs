using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.IService.IProductsService
{
    public interface IProductBundleService
    { 
        Task<APIResponseDto?> CreateAsync(ProductBundleCreateDto dto);
        Task<APIResponseDto?> UpdateAsync(ProductBundleUpdateDto dto);
        Task<APIResponseDto?> DeleteAsync(Guid bundleId);
        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> DeleteAllAsync();
         
        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetActiveAsync();
        Task<APIResponseDto?> GetByIdAsync(Guid bundleId);
        Task<APIResponseDto?> GetByParentProductAsync(Guid parentProductId);
        Task<APIResponseDto?> GetByComponentProductAsync(Guid componentProductId);
        Task<APIResponseDto?> GetByUnitAsync(Guid unitOfMeasurementId);
        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<APIResponseDto?> GetWithParentAsync(Guid bundleId);
        Task<APIResponseDto?> GetWithComponentAsync(Guid bundleId);
        Task<APIResponseDto?> GetWithUnitAsync(Guid bundleId);
        Task<APIResponseDto?> GetCountAsync();
        Task<APIResponseDto?> GetCountByParentAsync(Guid parentProductId);
        Task<APIResponseDto?> GetCountByComponentAsync(Guid componentProductId);
    }
}
