using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories.ICRMRepositories;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.TenantManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Repositories.CRMRepositories
{
    /// <summary>
    /// Repo for handling Supplier entities with multi-tenant support.
    /// Inherits CRUD, bulk, soft‑delete/restore, validation and existence logic from PersonRepository&lt;T&gt;.
    /// Adds only Supplier‑specific methods.
    /// </summary>
    public class SupplierRepository : PersonRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IUserProvider userProvider,
            IIncludeValidator<Supplier> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        { }
        public Task<bool> ExistsBySupplierCodeAsync(string supplierCode, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(supplierCode))
                return Task.FromResult(false);
            var code = supplierCode.Trim().ToLowerInvariant();
            return ExistsAsync(s => s.SupplierCode.ToLower() == code, ct);
        }

    }
}
