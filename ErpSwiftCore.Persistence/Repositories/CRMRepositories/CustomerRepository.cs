using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories.ICRMRepositories;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context; 
using ErpSwiftCore.TenantManagement.Interfaces; 
namespace ErpSwiftCore.Persistence.Repositories.CRMRepositories
{ 
    public class CustomerRepository : PersonRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<Customer> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        { }
        #region Existence
        public Task<bool> ExistsByCustomerCodeAsync(string customerCode, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
                return Task.FromResult(false);
            var code = customerCode.Trim().ToLowerInvariant();
            return ExistsAsync(c => c.CustomerCode.ToLower() == code, ct);
        }
        #endregion
    }
}
