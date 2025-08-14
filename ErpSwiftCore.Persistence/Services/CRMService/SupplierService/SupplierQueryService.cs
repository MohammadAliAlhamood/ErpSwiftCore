using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CRMService.SupplierService
{
    public class SupplierQueryService : ISupplierQueryService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;

        public SupplierQueryService(IMultiTenantUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Supplier.GetByIdAsync(supplierId, cancellationToken);
        }

        public async Task<IReadOnlyList<Supplier>> GetAllSuppliersAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Supplier.GetAllAsync(cancellationToken: cancellationToken);
        }


    }
}
