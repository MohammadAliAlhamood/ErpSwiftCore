using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CRMService.CustomerService
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public CustomerQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Customer.GetByIdAsync(customerId,   cancellationToken);
        }
        public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Customer.GetAllAsync(   cancellationToken: cancellationToken    );
        }
    }
}
