using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Abstractions;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Core;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.TenantManagement.Interfaces;

namespace ErpSwiftCore.Persistence.Repositories
{
    /// <summary>
    /// Generic repository for Person-derived entities (e.g., Customer, Supplier)
    /// with multi-tenant support, CRUD, bulk operations, soft-delete/restore,
    /// existence checks and validation.
    /// </summary>
    public class PersonRepository<T> : MultiTenantRepository<T>, IPersonRepository<T>
        where T : Person
    {
        public PersonRepository(AppDbContext db, ITenantProvider tenantProvider, IUserProvider userProvider, IIncludeValidator<T> includeValidator)
            : base(db, tenantProvider, userProvider, includeValidator)
        {
        }

        public async Task<Guid> CreateAsync(T entity, CancellationToken ct = default)
        {
            try
            {
                await AddAsync(entity, autoSave: true, ct);
                return entity.ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }
        public async Task<IEnumerable<Guid>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            var ids = new List<Guid>();
            foreach (var e in entities)
            {
                var id = await CreateAsync(e, ct);
                if (id != Guid.Empty)
                    ids.Add(id);
            }
            return ids;
        }
        public async Task<int> BulkImportAsync(IEnumerable<T> entities, CancellationToken ct = default)
        {
            try
            {
                await AddRangeAsync(entities, ct);
                return entities.Count();
            }
            catch
            {
                return 0;
            }
        }
        public async Task<bool> UpdateAsync(T entity, CancellationToken ct = default)
        {
            try
            {
                await base.UpdateAsync(entity, autoSave: true, ct);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
            => ProcessBatchAsync(new[] { id }, DeleteOneAsync, ct);
        private async Task<bool> DeleteOneAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var entity = await GetAsync(e => e.ID == id, ct);
                if (entity == null) return false;
                await SoftDeleteAsync(entity, autoSave: true, ct);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
            => ProcessBatchAsync(ids, DeleteOneAsync, ct);
        public Task<bool> DeleteAllAsync(CancellationToken ct = default)
            => ProcessBatchAsync(
                GetAllAsync(cancellationToken: ct).Result.Select(e => e.ID),
                DeleteOneAsync,
                ct);

        public Task<bool> RestoreAsync(Guid id, CancellationToken ct = default)
            => ProcessBatchAsync(new[] { id }, RestoreOneAsync, ct);
        private async Task<bool> RestoreOneAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var entity = await GetOneSoftDeletedAsync(e => e.ID == id, ct);
                if (entity == null) return false;
                await RestoreAsync(entity, autoSave: true, ct);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> RestoreRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
            => ProcessBatchAsync(ids, RestoreOneAsync, ct);
        public Task<bool> RestoreAllAsync(CancellationToken ct = default)
            => ProcessBatchAsync(
                GetAllSoftDeletedAsync(cancellationToken: ct).Result.Select(e => e.ID),
                RestoreOneAsync,
                ct);


        public Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default)
            => ExistsAsync(e => e.ID == id, ct);
        public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Task.FromResult(false);

            return ExistsAsync(
                p => p.ContactInfo != null
                     && !string.IsNullOrWhiteSpace(p.ContactInfo.Email)
                     && p.ContactInfo.Email.ToLower() == email.Trim().ToLowerInvariant(),
                ct);
        }
        public Task<bool> ExistsByNationalIdAsync(string nationalId, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return Task.FromResult(false);

            var normalized = nationalId.Trim().ToLowerInvariant();
            return ExistsAsync(
                p => p.NationalID != null
                     && p.NationalID.ToLower() == normalized,
                ct);
        }
        public Task<bool> ExistsByPhoneAsync(string phone, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Task.FromResult(false);

            var digits = Regex.Replace(phone, "\\D", "");
            if (string.IsNullOrWhiteSpace(digits))
                return Task.FromResult(false);

            return ExistsAsync(
                p => p.ContactInfo != null
                     && !string.IsNullOrWhiteSpace(p.ContactInfo.Phone)
                     && Regex.Replace(p.ContactInfo.Phone, "\\D", "") == digits,
                ct);
        }
   
        public Task<bool> ExistsByEmailAsync(string email, Guid? excludingId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Task.FromResult(false);

            email = email.Trim().ToLowerInvariant();
            return ExistsAsync(
                p => p.ContactInfo != null
                     && !string.IsNullOrWhiteSpace(p.ContactInfo.Email)
                     && p.ContactInfo.Email.ToLower() == email
                     && (!excludingId.HasValue || p.ID != excludingId.Value),
                ct);
        }
        public Task<bool> ExistsByNationalIdAsync(string nationalId, Guid? excludingId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(nationalId))
                return Task.FromResult(false);

            var normalized = nationalId.Trim().ToLowerInvariant();
            return ExistsAsync(
                p => p.NationalID != null
                     && p.NationalID.ToLower() == normalized
                     && (!excludingId.HasValue || p.ID != excludingId.Value),
                ct);
        }
        public Task<bool> ExistsByPhoneAsync(string phone, Guid? excludingId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Task.FromResult(false);

            var digits = Regex.Replace(phone, "\\D", "");
            if (string.IsNullOrWhiteSpace(digits))
                return Task.FromResult(false);

            return ExistsAsync(
                p => p.ContactInfo != null
                     && !string.IsNullOrWhiteSpace(p.ContactInfo.Phone)
                     && Regex.Replace(p.ContactInfo.Phone, "\\D", "") == digits
                     && (!excludingId.HasValue || p.ID != excludingId.Value),
                ct);
        }
      
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }
        public bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var digits = Regex.Replace(phone, "\\D", "");
            return Regex.IsMatch(digits, "^\\d{7,15}$");
        }
    
        public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => GetAsync(e => e.ID == id, ct);
        public Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, CancellationToken ct = default)
            => GetAsync(filter, ct);
        public Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
            => base.GetAllAsync(filter, cancellationToken);
        public Task<IReadOnlyList<T>> GetDeletedAllAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
            => GetAllSoftDeletedAsync(filter, cancellationToken);




        private async Task<bool> ProcessBatchAsync<TArg>(
            IEnumerable<TArg> args,
            Func<TArg, CancellationToken, Task<bool>> operation,
            CancellationToken ct)
        {
            var results = await Task.WhenAll(args.Select(arg => operation(arg, ct)));
            return results.All(r => r);
        }

    }
}
