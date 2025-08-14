using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Service;
namespace ErpSwiftCore.Web.IService.ICRMsService
{
    public interface ISupplierService
    {
        Task<APIResponseDto?> GetAllAsync();

        Task<APIResponseDto?> GetByIdAsync(Guid id);

        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);


        Task<APIResponseDto?> CheckExistsAsync(Guid id);

        Task<APIResponseDto?> CheckExistsByCodeAsync(string supplierCode);

        Task<APIResponseDto?> CheckExistsByEmailAsync(string email);

        Task<APIResponseDto?> CheckExistsByEmailExcludingAsync(string email, Guid excludingId);

        Task<APIResponseDto?> CheckExistsByNationalIdAsync(string nationalId);

        Task<APIResponseDto?> CheckExistsByNationalIdExcludingAsync(string nationalId, Guid excludingId);

        Task<APIResponseDto?> CheckExistsByPhoneAsync(string phone);

        Task<APIResponseDto?> CheckExistsByPhoneExcludingAsync(string phone, Guid excludingId);

        Task<APIResponseDto?> CheckIsUniqueAsync(
            string code, string? email, string nationalId, string? phone);

        Task<APIResponseDto?> CheckIsUniqueExcludingAsync(
            Guid excludingId, string code, string? email, string nationalId, string? phone);

        // ─────────────── Command Methods ───────────────

        Task<APIResponseDto?> CreateAsync(CreateSupplierDto dto);

        Task<APIResponseDto?> UpdateAsync(UpdateSupplierDto dto);

        Task<APIResponseDto?> DeleteAsync(Guid id);

        Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);

        Task<APIResponseDto?> RestoreAsync(Guid id);

        Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);

    }
}
