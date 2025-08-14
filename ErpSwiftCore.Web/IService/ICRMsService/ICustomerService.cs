 
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.ICRMsService
{
    public interface ICustomerService
    {
        // ─────────────── Query Methods ───────────────

        Task<APIResponseDto?> GetAllAsync();
        Task<APIResponseDto?> GetByIdAsync(Guid id);

        Task<APIResponseDto?> GetByIdsAsync(IEnumerable<Guid> ids);
      


       Task<APIResponseDto?> CheckExistsAsync(Guid id);

        Task<APIResponseDto?> CheckExistsByCodeAsync(string customerCode);

        Task<APIResponseDto?> CheckExistsByEmailAsync(string email);

        Task<APIResponseDto?> CheckExistsByEmailExcludingAsync(string email, Guid excludingId);

        Task<APIResponseDto?> CheckExistsByNationalIdAsync(string nationalId);

        Task<APIResponseDto?> CheckExistsByNationalIdExcludingAsync(string nationalId, Guid excludingId);

        Task<APIResponseDto?> CheckExistsByPhoneAsync(string phone);

        Task<APIResponseDto?> CheckExistsByPhoneExcludingAsync(string phone, Guid excludingId);



        // ─────────────── Command Methods ───────────────

         Task<APIResponseDto?> CreateAsync(CreateCustomerDto dto);
        
         Task<APIResponseDto?> UpdateAsync(UpdateCustomerDto dto);
        
         Task<APIResponseDto?> DeleteAsync(Guid id);
        
         Task<APIResponseDto?> DeleteRangeAsync(IEnumerable<Guid> ids);
        
         Task<APIResponseDto?> RestoreAsync(Guid id);
        
         Task<APIResponseDto?> RestoreRangeAsync(IEnumerable<Guid> ids);
    }
}
