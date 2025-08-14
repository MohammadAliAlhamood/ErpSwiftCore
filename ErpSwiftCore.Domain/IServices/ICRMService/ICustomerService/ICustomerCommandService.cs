using ErpSwiftCore.Domain.Entities.EntityCRM;
namespace ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService
{
    public interface ICustomerCommandService
    {
        Task<Guid> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> AddCustomersAsync(IEnumerable<Customer> customers, CancellationToken cancellationToken = default);
        Task<bool> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<bool> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<bool> DeleteCustomersRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default);
        Task<bool> DeleteAllCustomersAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<bool> RestoreCustomersRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default);
        Task<bool> RestoreAllCustomersAsync(CancellationToken cancellationToken = default);
        
    }
}
