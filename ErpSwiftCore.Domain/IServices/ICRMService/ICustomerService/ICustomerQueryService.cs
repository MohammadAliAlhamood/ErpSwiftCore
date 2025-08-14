using ErpSwiftCore.Domain.Entities.EntityCRM;
namespace ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService
{
    public interface  ICustomerQueryService
    {
      Task<Customer?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken = default);
      Task<IReadOnlyList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default);

    }
}