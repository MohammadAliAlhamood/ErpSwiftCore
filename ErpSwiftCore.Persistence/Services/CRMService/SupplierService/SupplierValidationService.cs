using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CRMService.SupplierService
{
    public class SupplierValidationService : ISupplierValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public SupplierValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> SupplierExistsAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Supplier.ExistsByIdAsync(supplierId, cancellationToken);
        }
        public async Task<bool> SupplierExistsByCodeAsync(string supplierCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(supplierCode))
                return false;

            return await _unitOfWork.Supplier.ExistsBySupplierCodeAsync(supplierCode, cancellationToken);
        }
        public async Task<bool> SupplierExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _unitOfWork.Supplier.ExistsByEmailAsync(email, cancellationToken);
        }
        public async Task<bool> SupplierExistsByEmailAsync(string email, Guid excludingId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _unitOfWork.Supplier.ExistsByEmailAsync(email, excludingId, cancellationToken);
        }
        public async Task<bool> SupplierExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return await _unitOfWork.Supplier.ExistsByNationalIdAsync(nationalId, cancellationToken);
        }
        public async Task<bool> SupplierExistsByNationalIdAsync(string nationalId, Guid excludingId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return await _unitOfWork.Supplier.ExistsByNationalIdAsync(nationalId, excludingId, cancellationToken);
        }
        public async Task<bool> SupplierExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return await _unitOfWork.Supplier.ExistsByPhoneAsync(phone, cancellationToken);
        }
        public async Task<bool> SupplierExistsByPhoneAsync(string phone, Guid excludingId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return await _unitOfWork.Supplier.ExistsByPhoneAsync(phone, excludingId, cancellationToken);
        }

        public async Task<bool> SupplierIsUniqueAsync(
            string supplierCode,
            string email,
            string nationalId,
            string phone,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(supplierCode)
                && await _unitOfWork.Supplier.ExistsBySupplierCodeAsync(supplierCode.Trim(), cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(email)
                && await _unitOfWork.Supplier.ExistsByEmailAsync(email.Trim(), cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(nationalId)
                && await _unitOfWork.Supplier.ExistsByNationalIdAsync(nationalId.Trim(), cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(phone)
                && await _unitOfWork.Supplier.ExistsByPhoneAsync(phone.Trim(), cancellationToken))
                return false;

            return true;
        }
         
        public async Task<bool> SupplierIsUniqueAsync(
            Guid excludingId,
            string supplierCode,
            string email,
            string nationalId,
            string phone,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(supplierCode)
                && await _unitOfWork.Supplier.ExistsBySupplierCodeAsync(supplierCode.Trim(), cancellationToken))
                // כאן לא צריך excludingId כי קוד הספק ייחודי במערכת ואין overload עם excluding
                return false;

            if (!string.IsNullOrWhiteSpace(email)
                && await _unitOfWork.Supplier.ExistsByEmailAsync(email.Trim(), excludingId, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(nationalId)
                && await _unitOfWork.Supplier.ExistsByNationalIdAsync(nationalId.Trim(), excludingId, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(phone)
                && await _unitOfWork.Supplier.ExistsByPhoneAsync(phone.Trim(), excludingId, cancellationToken))
                return false;

            return true;
        }
  
    
    
    }
}