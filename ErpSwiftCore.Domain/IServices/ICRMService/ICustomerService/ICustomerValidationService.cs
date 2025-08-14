using ErpSwiftCore.Domain.IRepositories;

namespace ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService
{
    public interface ICustomerValidationService
    {
        Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByCodeAsync(string customerCode, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByEmailAsync(string email, Guid excludingCustomerId, CancellationToken cancellationToken = default);
        Task<bool> CustomerExistsByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByNationalIdAsync(string nationalId, Guid excludingCustomerId, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByPhoneAsync(string phone, CancellationToken cancellationToken = default);

        Task<bool> CustomerExistsByPhoneAsync(string phone, Guid excludingCustomerId, CancellationToken cancellationToken = default);

         
        Task<bool> CustomerIsUniqueAsync(string customerCode, string email,
        string nationalId, string phone, CancellationToken cancellationToken = default);
        Task<bool> CustomerIsUniqueAsync(Guid excludingId, string customerCode, string email,
          string nationalId, string phone, CancellationToken cancellationToken = default);
    }
}
