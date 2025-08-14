using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using ErpSwiftCore.Persistence.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CRMService.CustomerService
{
    public class CustomerValidationService : ICustomerValidationService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public CustomerValidationService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 1. تحقق بوجود عميل حسب المعرف
        public async Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Customer.ExistsByIdAsync(customerId, cancellationToken);
        }

        // 2. تحقق بوجود عميل حسب رمز العميل
        public async Task<bool> CustomerExistsByCodeAsync(string customerCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
                return false;

            return await _unitOfWork.Customer.ExistsByCustomerCodeAsync(customerCode.Trim(), cancellationToken);
        }

        // 3. تحقق بوجود عميل حسب البريد الإلكتروني
        public async Task<bool> CustomerExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // للتحديث يمكن تمرير المعرف في excludingCustomerId
            return await _unitOfWork.Customer.ExistsByEmailAsync(
                email.Trim(),
                excludingId: null,
                cancellationToken);
        }

        // 4. تحقق بوجود عميل حسب البريد الإلكتروني مع استثناء معرف
        public async Task<bool> CustomerExistsByEmailAsync(string email, Guid excludingCustomerId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _unitOfWork.Customer.ExistsByEmailAsync(
                email.Trim(),
                excludingCustomerId,
                cancellationToken);
        }

        // 5. تحقق بوجود عميل حسب الرقم الوطني
        public async Task<bool> CustomerExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return await _unitOfWork.Customer.ExistsByNationalIdAsync(
                nationalId.Trim(),
                excludingId: null,
                cancellationToken);
        }

        // 6. تحقق بوجود عميل حسب الرقم الوطني مع استثناء معرف
        public async Task<bool> CustomerExistsByNationalIdAsync(string nationalId, Guid excludingCustomerId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return false;

            return await _unitOfWork.Customer.ExistsByNationalIdAsync(
                nationalId.Trim(),
                excludingCustomerId,
                cancellationToken);
        }

        // 7. تحقق بوجود عميل حسب رقم الهاتف
        public async Task<bool> CustomerExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return await _unitOfWork.Customer.ExistsByPhoneAsync(
                phone.Trim(),
                excludingId: null,
                cancellationToken);
        }

        // 8. تحقق بوجود عميل حسب رقم الهاتف مع استثناء معرف
        public async Task<bool> CustomerExistsByPhoneAsync(string phone, Guid excludingCustomerId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return await _unitOfWork.Customer.ExistsByPhoneAsync(
                phone.Trim(),
                excludingCustomerId,
                cancellationToken);
        }


         
        public async Task<bool> CustomerIsUniqueAsync(
            string customerCode,
            string email,
            string nationalId,
            string phone,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(customerCode)
                && await _unitOfWork.Customer.ExistsByCustomerCodeAsync(customerCode.Trim(), cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(email)
                && await _unitOfWork.Customer.ExistsByEmailAsync(email.Trim(), null, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(nationalId)
                && await _unitOfWork.Customer.ExistsByNationalIdAsync(nationalId.Trim(), null, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(phone)
                && await _unitOfWork.Customer.ExistsByPhoneAsync(phone.Trim(), null, cancellationToken))
                return false;

            return true;
        } 
        public async Task<bool> CustomerIsUniqueAsync(
            Guid excludingId,
            string customerCode,
            string email,
            string nationalId,
            string phone,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(customerCode)
                && await _unitOfWork.Customer.ExistsByCustomerCodeAsync(customerCode.Trim(), cancellationToken))
                // בגרסה הנוכחית אין overload של Code עם excludingId, ולכן קריאה רגילה מספיקה
                return false;

            if (!string.IsNullOrWhiteSpace(email)
                && await _unitOfWork.Customer.ExistsByEmailAsync(email.Trim(), excludingId, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(nationalId)
                && await _unitOfWork.Customer.ExistsByNationalIdAsync(nationalId.Trim(), excludingId, cancellationToken))
                return false;

            if (!string.IsNullOrWhiteSpace(phone)
                && await _unitOfWork.Customer.ExistsByPhoneAsync(phone.Trim(), excludingId, cancellationToken))
                return false;

            return true;
        }
    }
}