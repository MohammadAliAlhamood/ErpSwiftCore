using ErpSwiftCore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService
{
    public interface ISupplierValidationService
    {
        Task<bool> SupplierExistsAsync(Guid supplierId, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByCodeAsync(string supplierCode, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByEmailAsync(string email, Guid excludingId, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByNationalIdAsync(string nationalId, Guid excludingId, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default);
        Task<bool> SupplierExistsByPhoneAsync(string phone, Guid excludingId, CancellationToken cancellationToken = default);



          Task<bool> SupplierIsUniqueAsync(
         string supplierCode,
         string email,
         string nationalId,
         string phone,
         CancellationToken cancellationToken = default);

        Task<bool> SupplierIsUniqueAsync(
            Guid excludingId,
            string supplierCode,
            string email,
            string nationalId,
            string phone,
            CancellationToken cancellationToken = default);

    }
}
